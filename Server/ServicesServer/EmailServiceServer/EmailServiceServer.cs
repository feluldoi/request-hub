using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using RequestHub.Shared;
using MailKit.Net.Smtp;

namespace RequestHub.Server.ServicesServer.EmailServiceServer
{
    public class EmailServiceServer : IEmailServiceServer
    {
        private readonly IConfiguration _config;

        public EmailServiceServer(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ServiceResponse<string>> SendEmail(EmailDto request)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Smtp:Username").Value));//from appsettings.json
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("Smtp:Host").Value, _config.GetSection("Smtp:Port").Get<Int16>(), SecureSocketOptions.StartTls);//from appsettings.json
            smtp.Authenticate(_config.GetSection("Smtp:Username").Value, _config.GetSection("Smtp:Password").Value);//from appsettings.json
            smtp.Send(email);
            smtp.Disconnect(true);

            return new ServiceResponse<string>
            {
                Message = "Email sent successfully."
            };

        }

    }
}
