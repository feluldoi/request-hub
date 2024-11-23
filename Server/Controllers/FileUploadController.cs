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

        [HttpGet("ticket/{ticketId}")]
        public async Task<ActionResult<List<UploadFile>>> GetTicketUploadFiles(int ticketId)
        {
            try
            {
                if (ticketId == 0)
                {
                    _logger.LogWarning("Invalid ticketId: 0");//developer feedback
                    return BadRequest("Invalid ticketId");//client feedback 
                }

                var ticketUploadFiles = await _fileUploadService.GetTicketUploadFilesAsync(ticketId);
                return Ok(ticketUploadFiles);//returns OK200 with ticket upload files

            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument provided");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting upload files associated with ticketId: {TicketId}", ticketId);
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
