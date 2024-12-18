using RequestHub.Shared;
using System.Net.Http.Json;

namespace RequestHub.Client.Services.AuthServiceClient
{
    public class AuthServiceClient : IAuthServiceClient
    {
        private readonly HttpClient _http;
        private readonly ILogger<AuthServiceClient> _logger;

        public AuthServiceClient(HttpClient http, ILogger<AuthServiceClient> logger)
        {
            _http = http;
            _logger = logger;
        }


        //new one
        public async Task<ServiceResponse<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/reset-password", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }


        ////added PasswordReset
        public async Task<ServiceResponse<string>> ForgotPassword(string email)
        {

            var result = await _http.PostAsJsonAsync("api/auth/forgot-password", new { Email = email });
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }


        public async Task<ServiceResponse<string>> VerifyEmail(string token)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<string>>($"api/auth/verify?token={token}");
            return result;
        }

        public async Task<ServiceResponse<string>> VerifyEmailResetPassTok(string token)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<string>>($"api/auth/verify-reset-pass-tok?token={token}");
            return result;
        }



        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> Register(UserRegister request)
        {
            _logger.LogInformation("TODO: in authServiceClient register api request");
            
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}
