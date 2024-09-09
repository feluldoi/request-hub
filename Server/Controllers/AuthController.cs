using Microsoft.AspNetCore.Mvc;
using RequestHub.Server.ServicesServer.AuthServiceServer;
using RequestHub.Shared;

namespace RequestHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceServer _authService;

        public AuthController(IAuthServiceServer authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {

            var response = await _authService.Register(
                new User
                {
                    Email = request.Email,
                    //post to the db
                    RequestorName = request.RequestorName,

                },
                 request.Password);

            if (!response.Success)
            {
                return Redirect("/verify-email/" + response.Message);
            }

            return BadRequest(response);

        }



        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        //new ResetPassword
        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var response = await _authService.ResetPassword(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        //added PasswordReset
        [HttpGet("verify")]
        public async Task<ActionResult<ServiceResponse<string>>> VerifyEmail([FromQuery] string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid token");
            }

            var result = await _authService.VerifyEmail(token);

            if (result.Success)
            {

                return Ok(result);
                //return Ok($"/verify-email/{token});
            }
            else
            {
                return BadRequest(result);
            }

        }


        //added PasswordReset
        [HttpGet("verify-reset-pass-tok")]
        public async Task<ActionResult<ServiceResponse<string>>> VerifyEmailResetPassTok([FromQuery] string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid token (verify-reset-pass-tok)");
            }

            var result = await _authService.VerifyEmailResetPassTok(token);

            if (result.Success)
            {

                //return Ok(result);
                return (Ok(result));
            }

            else
            {
                return BadRequest(result);
            }

        }


        //do not change this is correct
        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            //validate request here if necessary

            var response = await _authService.ForgotPassword(request.Email);

            if (!response.Success)
            {
                return BadRequest(response);
            }


            return Ok(response);
        }



    }
}
