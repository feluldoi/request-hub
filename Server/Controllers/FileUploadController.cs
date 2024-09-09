using Microsoft.AspNetCore.Mvc;
using RequestHub.Server.ServicesServer.FileUploadServiceServer;
using RequestHub.Shared;

namespace RequestHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadServiceServer _fileUploadService;
        private readonly ILogger<FileUploadController> _logger;

        //Ctor
        public FileUploadController(IFileUploadServiceServer fileUploadService, ILogger<FileUploadController> logger)
        {
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<UploadFile>> Upload(IFormFile file)
        {
            try
            {
                var uploadedFile = await _fileUploadService.UploadFileAsync(file);
                return Ok(uploadedFile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File upload failed");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UploadFile>>> GetUploadedFiles()
        {
            try
            {
                var files = await _fileUploadService.GetUploadedFilesAsync();
                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting uploaded files");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
