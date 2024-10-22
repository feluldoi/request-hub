using Microsoft.AspNetCore.Mvc;
using RequestHub.Server.ServicesServer.EmailServiceServer;
using RequestHub.Shared;

namespace RequestHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailServiceServer _emailService;

        public EmailController(IEmailServiceServer emailService)
        {
            _emailService = emailService;
        }


        [HttpPost("send")]
        public ActionResult<ServiceResponse<string>> SendEmail(EmailDto request)
        {
            _emailService.SendEmail(request);

            //handle the response with this code!
            var response = new
            {
                success = true,
                message = "Email sent successfully."
            };
            return Ok(response);

        }

    }
}
