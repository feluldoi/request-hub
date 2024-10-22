using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestHub.Server.Data;
using RequestHub.Server.ServicesServer.EmailServiceServer;
using RequestHub.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RequestHub.Server.ServicesServer.AuthServiceServer
{
    public class AuthServiceServer : IAuthServiceServer
    {
        //Constructor/initialize
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailServiceServer _emailService;

        public AuthServiceServer(DataContext context, IConfiguration configuration, IEmailServiceServer emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
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



        //Used with SendVerificationEmail method
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                //response for UserExists
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User Already Exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            //added PasswordReset
            user.VerificationToken = CreateRandomToken();

            //store user object in db
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await SendVerificationEmail(user);

            //response for SendVerificationEmail
            return new ServiceResponse<int>
            {
                Success = true,
            };
        }


        //Used with Register method
        public async Task<ServiceResponse<string>> SendVerificationEmail(User user)
        {

            var verificationLink = $"https://localhost:7035/verify-email/{user.VerificationToken}";

            var htmlLink = new MarkupString($"<a href=\"{verificationLink}\" target=\"_blank\">Click her to verify your account</a>");

            var emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Account Verification",
                Body = $"Click the following link to verify your account: {htmlLink}"

            };

            await _emailService.SendEmail(emailDto);

            return new ServiceResponse<string> { Message = "Verification email sent successfully." };
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

            var forgotPasswordLink = $"https://localhost:7035/verify-reset-pass-tok/{user.PasswordResetToken}";

            var htmlLink = new MarkupString($"<a href=\"{forgotPasswordLink}\" target=\"_blank\">Click her to reset your password</a>");

            var emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Password Recovery",
                Body = $"Click the following link to reset your password: {htmlLink}"

            };

            await _emailService.SendEmail(emailDto);

            return new ServiceResponse<string> { Message = "Forgot password email sent successfully." };
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





        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower()
            .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }



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



    }
}
