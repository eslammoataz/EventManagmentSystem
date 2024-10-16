using EventManagmentSystem.Application.Commands.UserCommands.CompleteUserProfile;
using EventManagmentSystem.Application.Commands.UserCommands.DeleteUser;
using EventManagmentSystem.Application.Commands.UserCommands.RegisterUser;
using EventManagmentSystem.Application.Queries.UserQueries.GetAllUsers;
using EventManagmentSystem.Application.Queries.UserQueries.GetUserById;
using EventManagmentSystem.Application.Queries.UserQueries.GetUserProfile;
using EventManagmentSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="command">The details of the user to be created.</param>
        /// <returns>Returns the created user details or error message.</returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error.Message);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            if (result.IsFailure)
            {
                return BadRequest(result.Error.Message);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>Returns the user details or not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id });

            if (result.IsFailure)
            {
                return NotFound(result.Error.Message);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns success or failure message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });

            if (result.IsFailure)
            {
                return BadRequest(result.Error.Message);
            }

            return Ok(new { message = "User deleted successfully." });
        }

        [HttpPost("CompleteUserProfile")]
        public async Task<IActionResult> CompleteUserProfile(CompleteUserProfileCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetUserProfile")]
        [Authorize(AuthenticationSchemes = "CustomToken")]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            var userId = user.Id;

            var query = new GetUserProfileQuery(userId);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
