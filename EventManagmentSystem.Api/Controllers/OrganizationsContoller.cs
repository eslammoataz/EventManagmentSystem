using EventManagmentSystem.Application.Commands.OrganizationCommands.AddUsersToOrganization;
using EventManagmentSystem.Application.Commands.OrganizationCommands.CreateOrganization;
using EventManagmentSystem.Application.Commands.OrganizationCommands.DeleteOrganization;
using EventManagmentSystem.Application.Commands.OrganizationCommands.RemoveUsersFromOrganization;
using EventManagmentSystem.Application.Queries.OrganizationQueries.GetAllOrganizations;
using EventManagmentSystem.Application.Queries.OrganizationQueries.GetOrganizationByAdminId;
using EventManagmentSystem.Application.Queries.OrganizationQueries.GetOrganizationById;
using EventManagmentSystem.Application.Queries.OrganizationQueries.GetUsersByOrganization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new organization and assigns the provided user as the admin.
        /// </summary>
        /// <param name="command">The details of the organization and the admin user.</param>
        /// <returns>Returns a response indicating success or failure.</returns>
        [HttpPost()]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get all organizations.
        /// </summary>
        /// <returns>Returns a list of all organizations.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllOrganizations()
        {
            var result = await _mediator.Send(new GetAllOrganizationsQuery());

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get organization by ID.
        /// </summary>
        /// <param name="id">The organization ID.</param>
        /// <returns>Returns the organization details or not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationById(string id)
        {
            var result = await _mediator.Send(new GetOrganizationByIdQuery { Id = id });

            if (result.IsFailure)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete an organization by ID.
        /// </summary>
        /// <param name="id">The ID of the organization to delete.</param>
        /// <returns>Returns success or failure message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(string id)
        {
            var result = await _mediator.Send(new DeleteOrganizationCommand { Id = id });

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Add users to an organization.
        /// </summary>
        /// <param name="organizationId">The ID of the organization.</param>
        /// <param name="request">The request with admin and user IDs to add.</param>
        /// <returns>Returns success or failure message.</returns>
        [HttpPost("AddUsersToOrganization/{organizationId}")]
        public async Task<IActionResult> AddUsersToOrganization(string organizationId,
            [FromBody] AddUsersToOrganizationRequest request)
        {
            var command = new AddUsersToOrganizationCommand(organizationId, request.AdminUserId, request.UserIds);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Remove users from an organization.
        /// </summary>
        /// <param name="organizationId">The ID of the organization.</param>
        /// <param name="request">The request with admin and user IDs to remove.</param>
        /// <returns>Returns success or failure message.</returns>
        [HttpPost("RemoveUsersFromOrganization/{organizationId}")]
        public async Task<IActionResult> RemoveUsersFromOrganization(string organizationId,
            [FromBody] RemoveUsersFromOrganizationRequest request)
        {
            var command = new RemoveUsersFromOrganizationCommand(organizationId, request.AdminUserId, request.UserIds);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("{organizationId}/users")]
        public async Task<IActionResult> GetUsersByOrganization(string organizationId)
        {
            var query = new GetUsersByOrganizationQuery { OrganizationId = organizationId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetOrganizationByAdminId/{adminId}")]
        public async Task<IActionResult> GetOrganizationByAdminId(string adminId)
        {
            var query = new GetOrganizationByAdminIdQuery { AdminId = adminId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
