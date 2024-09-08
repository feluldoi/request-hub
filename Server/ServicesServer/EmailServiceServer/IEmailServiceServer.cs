using RequestHub.Shared;

namespace RequestHub.Server.ServicesServer.EmailServiceServer
{
    public interface IEmailServiceServer
    {
        Task<ServiceResponse<string>> SendEmail(EmailDto request);
    }
}
