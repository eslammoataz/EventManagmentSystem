namespace EventManagmentSystem.Application.Dto.Events
{
    public class EventDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string EventType { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Status { get; set; }

    }
}
