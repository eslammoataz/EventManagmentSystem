namespace EventManagmentSystem.Domain.Models
{
    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // Navigation property to keep track of users in this organization
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        // The main admin of the organization
        public string AdminUserId { get; set; }
        public ApplicationUser AdminUser { get; set; }

        // List of events the organization manages
        public ICollection<Event> Events { get; set; }
    }

}
