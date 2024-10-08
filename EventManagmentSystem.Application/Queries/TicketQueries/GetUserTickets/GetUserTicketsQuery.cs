using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.TicketQueries.GetUserTickets
{
    public class GetUserTicketsQuery : IRequest<Result<List<TicketDto>>>
    {
        public string UserId { get; set; }

        public GetUserTicketsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
