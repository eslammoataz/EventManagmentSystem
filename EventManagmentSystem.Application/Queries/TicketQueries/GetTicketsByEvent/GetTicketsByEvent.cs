using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.TicketQueries.GetTicketsByEvent
{
    public class GetTicketsByEventQuery : IRequest<Result<List<TicketDto>>>
    {
        public string EventId { get; set; }
    }
}
