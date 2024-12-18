using RequestHub.Shared;

namespace RequestHub.Client.Services.AuthServiceClient
{
    public interface IAuthServiceClient
    {
        Task<ServiceResponse<string>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        //new one 
        Task<ServiceResponse<bool>> ResetPassword(ResetPasswordRequest request);
        //added for PasswordReset
        Task<ServiceResponse<string>> ForgotPassword(string email);
        //added for PasswordReset
        Task<ServiceResponse<string>> VerifyEmail(string token);
        Task<ServiceResponse<string>> VerifyEmailResetPassTok(string token);

    }
}
