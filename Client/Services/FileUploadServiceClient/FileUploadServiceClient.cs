using Microsoft.AspNetCore.Components.Forms;
using RequestHub.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RequestHub.Client.Services.FileUploadServiceClient
{
    public class FileUploadServiceClient : IFileUploadServiceClient
    {
        private readonly HttpClient _http;
        private readonly ILogger<FileUploadServiceClient> _logger;

        public FileUploadServiceClient(HttpClient http, ILogger<FileUploadServiceClient> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<UploadFile> UploadFileAsync(IBrowserFile file)
        {
            try
            {
                var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.Name);

                var response = await _http.PostAsync("api/FileUpload", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<UploadFile>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file: {FileName}", file.Name);
                throw;
            }
        }

        public async Task<List<UploadFile>> GetUploadedFilesAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<UploadFile>>("api/FileUpload");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting uploaded files");
                throw;
            }
        }
    }
}
