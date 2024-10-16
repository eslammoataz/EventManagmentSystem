using EventManagmentSystem.Application.Dto;
using EventManagmentSystem.Application.Services.Auth;
using EventManagmentSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "CustomToken")] // Custom authentication scheme
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _authService = authService;
            _userManager = userManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Generates an OTP token for the user based on the phone number provided.
        /// </summary>
        /// <param name="phoneNumber">The phone number of the user.</param>
        /// <returns>A response containing the OTP token or an error message.</returns>
        [AllowAnonymous]
        [HttpPost("GenerateOtpToken")]
        public async Task<IActionResult> GenerateOtpToken([FromBody] GenerateOtpTokenDto otpTokenDto)
        {
            var result = await _authService.GenerateOtpTokenAsync(otpTokenDto.PhoneNumber);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Validates the OTP token and returns an authentication token if successful.
        /// </summary>
        /// <param name="otpToken">The OTP token provided by the user.</param>
        /// <param name="phoneNumber">The phone number associated with the OTP token.</param>
        /// <returns>A response containing an authentication token or an error message.</returns>
        [AllowAnonymous]
        [HttpPost("ValidateOtpToken")]
        public async Task<IActionResult> ValidateOtpToken([FromBody] ValidateOtpTokenDto validateOtpTokenDto)
        {
            var result = await _authService.ValidateOtpTokenAsync(validateOtpTokenDto.OtpToken, validateOtpTokenDto.PhoneNumber);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Logs the user out by invalidating their authentication token.
        /// </summary>
        /// <returns>A response indicating whether the logout was successful or not.</returns>
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var tokenFromHeaders = HttpContext.Request.Headers["Authorization"].ToString();
            var result = await _authService.LogoutAsync(tokenFromHeaders, User);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}


