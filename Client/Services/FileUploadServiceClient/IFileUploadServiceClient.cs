using Microsoft.AspNetCore.Components.Forms;
using RequestHub.Shared;

namespace RequestHub.Client.Services.FileUploadServiceClient
{
    public interface IFileUploadServiceClient
    {
        Task<UploadFile> CreateUploadFile(IBrowserFile file, int ticketId);
        Task<List<UploadFile>> GetTicketFileUploads(int ticketId);
    }
}
