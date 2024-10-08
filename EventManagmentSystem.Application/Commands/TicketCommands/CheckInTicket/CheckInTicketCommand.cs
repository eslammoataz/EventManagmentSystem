using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.CheckInTicket
{
    public class CheckInTicketCommand : IRequest<Result>
    {
        public string TicketId { get; }

        public CheckInTicketCommand(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}
