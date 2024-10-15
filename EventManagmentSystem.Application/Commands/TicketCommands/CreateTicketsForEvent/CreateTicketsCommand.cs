using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.CreateTicketsForEvent
{
    public class CreateTicketsCommand : IRequest<Result<List<TicketDto>>>
    {
        public string EventId { get; set; }
        public string AdminUserId { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateTicketsRequest
    {
        public string EventId { get; set; }
        public string AdminUserId { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}
