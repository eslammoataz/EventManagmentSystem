namespace EventManagmentSystem.Domain.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Use the enum for Category
        public EventCategory Category { get; set; }

        public EventType EventType { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string? MeetingUrl { get; set; }

        // Navigation property linking the event to an organization
        public string OrganizerId { get; set; }
        public Organization Organizer { get; set; }

        // Event details
        public ICollection<Ticket> Tickets { get; set; }
        public EventStatus Status { get; set; }

    }

    public enum EventCategory
    {
        Conference,
        Sports,
        Music,
        Education,
        Workshop
    }

    public enum EventType
    {
        Online,
        Offline
    }
    public enum EventStatus
    {
        Draft,
        Published
    }


}
