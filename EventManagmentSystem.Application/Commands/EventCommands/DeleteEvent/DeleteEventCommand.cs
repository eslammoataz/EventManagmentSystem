using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.EventCommands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<Result>
    {
        public string EventId { get; }

        public DeleteEventCommand(string eventId)
        {
            EventId = eventId;
        }
    }
}
