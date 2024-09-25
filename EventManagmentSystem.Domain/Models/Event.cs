namespace EventManagmentSystem.Domain.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Use the enum for Category
        public EventCategory Category { get; set; }

        // Navigation property linking the event to an organization
        public string OrganizerId { get; set; }
        public Organization Organizer { get; set; }

        // Event details
        public ICollection<Ticket> Tickets { get; set; }
    }

    public enum EventCategory
    {
        Conference,
        Sports,
        Music,
        Education,
        Workshop
    }


}
