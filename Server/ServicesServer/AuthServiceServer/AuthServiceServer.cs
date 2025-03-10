﻿using Azure.Communication.Email;
using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestHub.Server.Data;
using RequestHub.Server.ServicesServer.EmailServiceServer;
using RequestHub.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace RequestHub.Server.ServicesServer.AuthServiceServer
{
    public class AuthServiceServer : IAuthServiceServer
    {
        //Constructor/initialize
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailServiceServer _emailService;
        private readonly ILogger<AuthServiceServer> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthServiceServer(DataContext context, IConfiguration configuration, IEmailServiceServer emailService, ILogger<AuthServiceServer> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<ServiceResponse<string>> Login(string email, string password)//JSON Web Tokens
        {

            try
            {
                var response = new ServiceResponse<string>();
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "user not found.";
                }
                else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "wrong password.";
                }
                //added PasswordReset
                else if (user.VerifiedAt == null)
                {
                    response.Success = false;
                    response.Message = "Not Verified!";
                }
                else
                {
                    response.Data = CreateToken(user);
                    response.Success = true;
                    response.Message = $"Welcome back, {user.Email}";
                };

                return response;
            }
            catch (Exception)
            {

                throw new Exception("Error in Login method");
            }

        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("JWT").Value));
            //.GetBytes(Environment.GetEnvironmentVariable("JWT"))); this does not work

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }


        #region Registration

        //Used with SendVerificationEmail method
        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            _logger.LogInformation("in Register method on AuthServiceServer.cs");

            var response = new ServiceResponse<string>();

            try
            {
                if (await UserExists(user.Email))
                {
                    response.Success = false;
                    response.Message = "User alreaddy exists.";
                }

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                //added PasswordReset
                user.VerificationToken = CreateRandomToken();

                //store user object in db
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("user added");

                var sendResult = await SendVerificationEmail(user);
                if (sendResult.Success == false)
                {

                    response.Success = false;
                    response.Message = "Email Failed to send ";

                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Registering user", ex);

            }

        }



        //Used with Register method
        public async Task<ServiceResponse<string>> SendVerificationEmail(User user)
        {

            #region MainKit Implementation
            //USING MAILKIT 
            //var htmlLink = new MarkupString($"<a href=\"{verificationLink}\" target=\"_blank\">Click her to verify your account</a>");

            //var emailDto = new EmailDto
            //{
            //    To = user.Email,
            //    Subject = "Account Verification",
            //    Body = $"Click the following link to verify your account: {htmlLink}"

            //};

            //await _emailService.SendEmail(emailDto);

            //return new ServiceResponse<string> { Message = "Verification email sent successfully." };


            //dev urls
            //var verificationLink = $"https://localhost:7035/verify-email/{user.VerificationToken}";//IIS 
            #endregion

            //Send Email From Azure Email Communication Service
            string verificationLink;
            var isDev = _webHostEnvironment.IsDevelopment();
            if (isDev == false)
            {
                verificationLink = $"https://requesthub.azurewebsites.net/verify-email/{user.VerificationToken}";
            }
            else
            {
                verificationLink = $"https://localhost:7252/verify-email/{user.VerificationToken}";
            }




            var emailConnectionString = _configuration["EMAIL_CONNECTIONSTRING"];

            //instantiate email client
            var emailClient = new EmailClient(emailConnectionString);
            // Create an email message. no need for dto since its built into the package
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@5a6f902e-689d-4ba3-9374-2e25083ee4da.azurecomm.net",
                content: new EmailContent("Verify Email")
                {
                    PlainText = "Hello from RequestHub Website",
                    Html = @$"
		                    <html>
		                    	<body>
		                    		<h1>Hello world via email.</h1>
                                    <a href=""{verificationLink}"" target =""_blank"" >Click here to verify your account</a>
		                    	</body>
		                    </html>"
                },
                recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(user.Email) }));

            //send message
            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Completed,
                emailMessage);
            var emailResult = emailSendOperation.GetRawResponse();
            var response = new ServiceResponse<string>();
            if (!emailResult.IsError)
            {
                response.Message = "Verification email sent successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Failed to send verification email.";
            }

            return response;
        }



        #endregion



        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower()
            .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }



        public async Task<ServiceResponse<string>> VerifyEmail(string token)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.VerificationToken == token);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid verification token.";
            }
            else
            {
                user.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                response.Message = "Email verified successfully.";
            };

            return response;
        }







        #region Forgot/Reset Password
        //used with SendForgotPasswordEmail method
        public async Task<ServiceResponse<string>> ForgotPassword(string email)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else
            {
                user.PasswordResetToken = CreateRandomToken();
                user.ResetTokenExpires = DateTime.Now.AddDays(1);
                await _context.SaveChangesAsync();
                response.Message = "Check your inbox.";

                //send email to the user with password reset token
                await SendForgotPasswordEmail(user);
            }

            return response;
        }



        //Used with ForgotPassword method
        public async Task<ServiceResponse<string>> SendForgotPasswordEmail(User user)
        {

            #region Using MailKit
            //var forgotPasswordLink = $"https://localhost:7035/verify-reset-pass-tok/{user.PasswordResetToken}";

            //var htmlLink = new MarkupString($"<a href=\"{forgotPasswordLink}\" target=\"_blank\">Click her to reset your password</a>");

            //var emailDto = new EmailDto
            //{
            //    To = user.Email,
            //    Subject = "Password Recovery",
            //    Body = $"Click the following link to reset your password: {htmlLink}"

            //};

            //await _emailService.SendEmail(emailDto);

            //return new ServiceResponse<string> { Message = "Forgot password email sent successfully." };
            #endregion

            //Send Email From Azure Email Communication Service
            string verificationLink;
            var isDev = _webHostEnvironment.IsDevelopment();
            if (isDev == false)
            {
                verificationLink = $"https://requesthub.azurewebsites.net/verify-reset-pass-tok/{user.PasswordResetToken}";
            }
            else
            {
                verificationLink = $"https://localhost:7252/verify-reset-pass-tok/{user.PasswordResetToken}";
            }




            var emailConnectionString = _configuration["EMAIL_CONNECTIONSTRING"];

            //instantiate email client
            var emailClient = new EmailClient(emailConnectionString);
            // Create an email message. no need for dto since its built into the package
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@5a6f902e-689d-4ba3-9374-2e25083ee4da.azurecomm.net",
                content: new EmailContent("Forgot Password")
                {
                    PlainText = "Hello from RequestHub Website",
                    Html = @$"
		                    <html>
		                    	<body>
		                    		<h1>Hello world via email.</h1>
                                    <a href=""{verificationLink}"" target =""_blank"" >Click here to reset your password</a>
		                    	</body>
		                    </html>"
                },
                recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(user.Email) }));

            //send message
            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Completed,
                emailMessage);
            var emailResult = emailSendOperation.GetRawResponse();
            var response = new ServiceResponse<string>();
            if (!emailResult.IsError)
            {
                response.Message = "Forgot password email sent successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Failed to send forgot password email.";
            }

            return response;




        }




        public async Task<ServiceResponse<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var response = new ServiceResponse<bool>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Invalid Token Or User does not exist";
            }

            else
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PasswordResetToken = null;
                user.ResetTokenExpires = null;


                await _context.SaveChangesAsync();

                //response.Message = "Password successfully reset.";
            }



            return response;
        }


        public async Task<ServiceResponse<string>> VerifyEmailResetPassTok(string token)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.PasswordResetToken == token);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid verification token.";
            }
            else
            {
                user.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                //response.Message = "Email verified successfully.";
            };

            return response;
        }



        #endregion














        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }




        // added PasswordReset
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }







        



    }
}
