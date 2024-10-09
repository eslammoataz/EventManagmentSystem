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
                return Ok(result.Value);
            }

            return BadRequest(result.Error.Message);
        }

        [Authorize(AuthenticationSchemes = "CustomToken")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            // Get the authenticated user using UserManager
            var user = await _userManager.GetUserAsync(User);

            // If the user is not found, return Unauthorized
            if (user == null)
            {
                return Unauthorized();
            }

            // Return the user's profile (UserName and Email in this case)
            return Ok(user);
        }


        ///// <summary>
        ///// Combines user and organization creation, handling both in one request.
        ///// </summary>
        ///// <param name="completeProfileAndOrganizationDto">The combined user and organization creation details.</param>
        ///// <returns>Returns success response with user and organization details.</returns>
        //[HttpPost("CompleteProfileAndCreateOrganization")]
        //public async Task<IActionResult> CompleteProfileAndCreateOrganization(
        //    [FromBody] CompleteProfileAndOrganizationDto completeProfileAndOrganizationDto)
        //{
        //    var userData = completeProfileAndOrganizationDto.User;

        //    var userCommand = new CreateUserCommand
        //    {
        //        Name = userData.FirstName + userData.LastName,
        //        Email = userData.Email,
        //        PhoneNumber = userData.PhoneNumber,
        //        UserName = userData.FirstName + userData.LastName
        //    };

        //    // 1. Create the user
        //    var userResult = await _mediator.Send(userCommand);

        //    if (userResult.IsFailure)
        //    {
        //        return BadRequest(userResult.Error.Message);
        //    }

        //    // 2. Create the organization and assign the user as admin

        //    var orgData = completeProfileAndOrganizationDto.Organization;

        //    var createOrganizationCommand = new CreateOrganizationCommand
        //    {
        //        OrganizationName = orgData.Name,
        //        AdminUserId = userResult.Value.UserId
        //    };

        //    var orgResult = await _mediator.Send(createOrganizationCommand);

        //    if (orgResult.IsFailure)
        //    {
        //        return BadRequest(orgResult.Error.Message);
        //    }

        //    // Combine User and Organization into a single DTO
        //    var combinedResult = new UserAndOrganizationDto
        //    {
        //        User = userResult.Value,
        //        Organization = orgResult.Value
        //    };

        //    // Return success response with both User and Organization details
        //    return Ok(Result.Success(combinedResult));
        //}

    }
}


