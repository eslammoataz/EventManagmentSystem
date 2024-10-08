using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Domain.Models;
using MediatR;

namespace EventManagmentSystem.Application.Commands.EventCommands.EditEvent
{
    public class EditEventCommand : IRequest<Result>
    {
        public string EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventCategory? Category { get; set; }

        public EventType? EventType { get; set; }
        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }
        public string? Longitude { get; set; }

        public string? Latitude { get; set; }

        public EditEventCommand(string eventId, string? title = null, string? description = null,
            DateTime? startDate = null, DateTime? endDate = null, EventCategory? category = null, EventType? eventType = null,
            string? imageUrl = null, string? videoUrl = null, string? longitude = null, string? latitude = null)
        {
            EventId = eventId;  // Event ID is required
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
            EventType = eventType;
            ImageUrl = imageUrl;
            VideoUrl = videoUrl;
            Longitude = longitude;
            Latitude = latitude;
        }
    }

    public class UpdateEventRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventCategory? Category { get; set; }
        public EventType? EventType { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }
    }
}
