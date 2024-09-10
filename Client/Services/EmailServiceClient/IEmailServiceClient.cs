using RequestHub.Shared;

namespace RequestHub.Client.Services.EmailServiceClient
{
    public interface IEmailServiceClient
    {
        Task<ServiceResponse<string>> SendEmail(EmailDto request);

    }
}
