using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Domain.Models;
using MediatR;

namespace EventManagmentSystem.Application.Commands.EventCommands.CreateEvent
{
    public class CreateEventCommand : IRequest<Result<EventDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventCategory Category { get; set; }
        public EventType EventType { get; set; }
        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string? MeetingUrl { get; set; }

        public string OrganizationId { get; set; } // Organization ID
        public string AdminUserId { get; set; } // The Admin User trying to create the event
        public List<TicketRequest> Tickets { get; set; } = new List<TicketRequest>();

    }

    public class CreateEventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventCategory Category { get; set; }
        public EventType EventType { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string? MeetingUrl { get; set; }
        public string OrganizationId { get; set; }
        public string AdminUserId { get; set; }
        public List<TicketRequest> Tickets { get; set; } = new List<TicketRequest>();

    }

    public class TicketRequest
    {
        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}
