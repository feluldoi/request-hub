using Microsoft.AspNetCore.Components.Forms;
using RequestHub.Shared;

namespace RequestHub.Client.Services.FileUploadServiceClient
{
    public interface IFileUploadServiceClient
    {
        Task<UploadFile> UploadFileAsync(IBrowserFile file);
        Task<List<UploadFile>> GetUploadedFilesAsync();
    }
}
