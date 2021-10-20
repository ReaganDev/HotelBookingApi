using System;
using hotel_booking_core.Interfaces;
using hotel_booking_dto;
using hotel_booking_dto.AuthenticationDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace hotel_booking_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;

        public AuthenticationController(ILogger logger, IAuthenticationService authService)
        {
            _logger = logger;
            _authService = authService;

        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<LoginResponseDto>>> Register([FromBody] RegisterUserDto model)
         {
            _logger.Information($"Registration Attempt for {model.Email}");
            var result = await _authService.Register(model);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> Login([FromBody] LoginDto model)
        {
            _logger.Information($"Login Attempt for {model.Email}");
            var result = await _authService.Login(model);
            return StatusCode(result.StatusCode, result);
        }



        [HttpPost]
        [Route("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> ConfirmEmail([FromBody] ConfirmEmailDto model)
        {
            var result = await _authService.ConfirmEmail(model);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPatch]
        [Route("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> ResetPassword([FromBody] ResetPasswordDto model)
        {
            _logger.Information($"Reset Password Attempt for {model.Email}");
            var result = await _authService.ResetPassword(model);
            return StatusCode(result.StatusCode, result);
        }


        [Authorize]
        [HttpPatch]
        [Route("update-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            var result = await _authService.UpdatePassword(model);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [Route("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<string>>> ForgotPassword(string email)
        {
            _logger.Information($"Forgot Password Attempt for {email}");

            var result = await _authService.ForgotPassword(email);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _authService.RefreshToken(refreshToken);
            setTokenCookie(response.ToString());
            return Ok(response);
        }


        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
