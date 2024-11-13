using Microsoft.EntityFrameworkCore;
using RequestHub.Server.Data;
using RequestHub.Shared;

namespace RequestHub.Server.ServicesServer.FileUploadServiceServer
{
    public class FileUploadServiceServer : IFileUploadServiceServer
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileUploadServiceServer> _logger;
        private readonly DataContext _context;

        //Ctor
        public FileUploadServiceServer(IWebHostEnvironment environment, ILogger<FileUploadServiceServer> logger, DataContext context)
        {
            _environment = environment;
            _logger = logger;
            _context = context;
        }

        public async Task<UploadFile> CreateUploadFileAsync(IFormFile file, int ticketId)
        {
            try
            {
                if (file.Length > 0)
                {
                    var trustedFileName = Path.GetRandomFileName();
                    var uploadPath = Path.Combine(_environment.ContentRootPath, "uploads");
                    var filePath = Path.Combine(uploadPath, trustedFileName);

                    //create upload folder
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    //Create uploaded file
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    var uploadFile = new UploadFile
                    {
                        FileName = file.FileName,
                        TrustedFileName = trustedFileName,
                        ContentType = file.ContentType,
                        Size = file.Length,
                        UploadDate = DateTime.UtcNow,
                        FilePath = filePath,
                        TicketId = ticketId

                    };
                    
                    //Add uploaded file to database
					_context.UploadFiles.Add(uploadFile);
                    await _context.SaveChangesAsync();


                    _logger.LogInformation("File saved: {Filename}", trustedFileName);

                    return uploadFile;
                }

                throw new ArgumentException("File is empty");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File upload failed");
                throw;
            }
        }


        public async Task<List<UploadFile>> GetUploadedFilesAsync()
        {
            try
            {
                return await _context.UploadFiles.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting uploaded files");
                throw;
            }
        }


    }
}
