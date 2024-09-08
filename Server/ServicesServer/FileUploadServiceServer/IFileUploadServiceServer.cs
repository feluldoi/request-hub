using RequestHub.Shared;

namespace RequestHub.Server.ServicesServer.FileUploadServiceServer
{
    public interface IFileUploadServiceServer
    {
        Task<UploadFile> UploadFileAsync(IFormFile file);
        Task<List<UploadFile>> GetUploadedFilesAsync();
    }
}
