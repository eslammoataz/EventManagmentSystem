using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Domain.Models;
using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.BookTicket
{
    public class BookTicketCommand : IRequest<Result>
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
        public TicketType TicketType { get; set; }

        public BookTicketCommand(string eventId, string userId, TicketType ticketType)
        {
            EventId = eventId;
            UserId = userId;
            TicketType = ticketType;
        }
    }

    public class BookTicketRequest
    {
        public required string EventId { get; set; }
        public required string UserId { get; set; }
        public required TicketType TicketType { get; set; }
    }
}
