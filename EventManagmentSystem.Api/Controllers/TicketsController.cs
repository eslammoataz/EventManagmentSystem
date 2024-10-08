using EventManagmentSystem.Application.Commands.TicketCommands.BookTicket;
using EventManagmentSystem.Application.Commands.TicketCommands.CheckInTicket;
using EventManagmentSystem.Application.Commands.TicketCommands.CreateTicketsForEvent;
using EventManagmentSystem.Application.Commands.TicketCommands.DeleteTickets;
using EventManagmentSystem.Application.Commands.TicketCommands.GenerateTicketQRCode;
using EventManagmentSystem.Application.Commands.TicketCommands.SendTicketAsGift;
using EventManagmentSystem.Application.Queries.TicketQueries.GetTicketsByEvent;
using EventManagmentSystem.Application.Queries.TicketQueries.GetUserTickets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("CreateTickets")]
        public async Task<IActionResult> CreateTickets([FromBody] CreateTicketsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateTicketsCommand
            {
                EventId = request.EventId,
                AdminUserId = request.AdminUserId,
                Type = request.Type,
                Price = request.Price,
                Quantity = request.Quantity
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("events/{eventId}/tickets")]
        public async Task<IActionResult> GetTicketsByEvent(string eventId)
        {
            var query = new GetTicketsByEventQuery { EventId = eventId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Value);
        }


        [HttpDelete("tickets/delete")]
        public async Task<IActionResult> DeleteTickets([FromBody] DeleteTicketsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new DeleteTicketsCommand(request.TicketIds);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookTicket([FromBody] BookTicketRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new BookTicketCommand(request.EventId, request.UserId, request.TicketType);

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("user/{userId}/tickets")]
        public async Task<IActionResult> GetUserTickets(string userId)
        {
            var query = new GetUserTicketsQuery(userId);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("generate-ticket-qr/{ticketId}")]
        public async Task<IActionResult> GenerateTicketQrCode(string ticketId)
        {
            var command = new GenerateTicketQrCodeCommand(ticketId);

            var qrCodeImage = await _mediator.Send(command);

            if (qrCodeImage == null)
            {
                return NotFound($"Ticket with ID {ticketId} not found.");
            }

            return File(qrCodeImage, "image/png");
        }

        [HttpGet("checkin/{ticketId}")]
        public async Task<IActionResult> CheckInTicket(string ticketId)
        {
            var command = new CheckInTicketCommand(ticketId);
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("send-gift")]
        public async Task<IActionResult> SendTicketAsGift([FromBody] SendTicketAsGiftRequest request)
        {
            var command = new SendTicketAsGiftCommand(
                senderUserId: request.SenderUserId,
                receiverUserId: request.ReceiverUserId,
                ticketId: request.TicketId
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error.Message });
            }

            return Ok(result.Value);
        }


    }
}
