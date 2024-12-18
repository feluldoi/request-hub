using RequestHub.Shared;

namespace RequestHub.Server.ServicesServer.AuthServiceServer
{
    public interface IAuthServiceServer
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<string>> VerifyEmail(string token);
        Task<ServiceResponse<string>> VerifyEmailResetPassTok(string token);
        Task<ServiceResponse<string>> ForgotPassword(string email);
        //new ResetPassword
        Task<ServiceResponse<bool>> ResetPassword(ResetPasswordRequest request);
        Task<ServiceResponse<string>> SendVerificationEmail(User user);
    }
}
