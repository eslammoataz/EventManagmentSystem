using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.TicketQueries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<Result<TicketDto>>
    {
        public string TicketId { get; set; }

        public GetTicketByIdQuery(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}
