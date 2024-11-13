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

        [HttpPost("ticket/{ticketId}")]
        public async Task<ActionResult<UploadFile>> CreateFileUpload(IFormFile file, int ticketId)
        {

            try
            {
                if (ticketId != 0)
                {
                    _logger.LogInformation($"ticket Id: {ticketId}");
                    var uploadedFile = await _fileUploadService.CreateUploadFileAsync(file, ticketId);
                    return Ok(uploadedFile);
                }
                else
                {
                    _logger.LogError($"cannot retrieve ticket id");
                    return StatusCode(500, "Internal server error");
                }

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
