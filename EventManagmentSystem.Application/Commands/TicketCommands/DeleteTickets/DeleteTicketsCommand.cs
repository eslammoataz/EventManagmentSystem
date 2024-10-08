using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.DeleteTickets
{
    public class DeleteTicketsCommand : IRequest<Result>
    {
        public List<string> TicketIds { get; set; }

        public DeleteTicketsCommand(List<string> ticketIds)
        {
            TicketIds = ticketIds;
        }
    }

    public class DeleteTicketsRequest
    {
        public List<string> TicketIds { get; set; }
    }


}
