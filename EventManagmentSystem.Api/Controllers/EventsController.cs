using EventManagmentSystem.Application.Commands.EventCommands.CreateEvent;
using EventManagmentSystem.Application.Commands.EventCommands.DeleteEvent;
using EventManagmentSystem.Application.Commands.EventCommands.EditEvent;
using EventManagmentSystem.Application.Queries.EventQueries.GetAllEvents;
using EventManagmentSystem.Application.Queries.EventQueries.GetEventById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var result = await _mediator.Send(new DeleteEventCommand(id));

            if (!result.IsSuccess)
            {

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, [FromBody] UpdateEventRequest request)
        {
            var command = new EditEventCommand(
                id,
                request.Title,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.Category,
                request.EventType,
                request.ImageUrl,
                request.VideoUrl,
                request.Longitude,
                request.Latitude,
                request.MeetingUrl,
                request.Status
            );

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var result = await _mediator.Send(new GetEventByIdQuery(id));

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
