using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.EventQueries.GetAllEvents
{
    public class GetAllEventsQuery : IRequest<Result<IEnumerable<EventDto>>>
    {
    }
}
