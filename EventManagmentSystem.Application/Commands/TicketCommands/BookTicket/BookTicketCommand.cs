using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.BookTicket
{
    public class BookTicketCommand : IRequest<Result>
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
        public string TicketType { get; set; }

        public BookTicketCommand(string eventId, string userId, string ticketType)
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
        public required string TicketType { get; set; }
    }
}
