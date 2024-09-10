using RequestHub.Shared;
using System.Net.Http.Json;

namespace RequestHub.Client.Services.EmailServiceClient
{
    public class EmailServiceClient : IEmailServiceClient
    {
        private readonly HttpClient _http;

        public EmailServiceClient(HttpClient http)
        {
            _http = http;

        }


        public async Task<ServiceResponse<string>> SendEmail(EmailDto request)
        {
            //making a post request 
            var result = await _http.PostAsJsonAsync("api/email/send", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

    }
}
