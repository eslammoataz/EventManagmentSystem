using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.EventQueries.GetEventById
{
    public class GetEventByIdQuery : IRequest<Result<EventDto>>
    {
        public string EventId { get; }

        public GetEventByIdQuery(string eventId)
        {
            EventId = eventId;
        }
    }
}
