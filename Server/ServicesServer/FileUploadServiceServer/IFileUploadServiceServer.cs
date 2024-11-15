using RequestHub.Shared;

namespace RequestHub.Server.ServicesServer.FileUploadServiceServer
{
    public interface IFileUploadServiceServer
    {
        Task<UploadFile> CreateUploadFileAsync(IFormFile file, int ticketId);
        Task<List<UploadFile>> GetTicketUploadFilesAsync(int ticketId);
    }
}
