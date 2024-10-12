using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagmentSystem.Domain.Models
{
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        // Navigation property to keep track of users in this organization
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        // The main admin of the organization
        public string AdminUserId { get; set; }
        public ApplicationUser AdminUser { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string ManagerName { get; set; }
        public string City { get; set; }
        public string Bio { get; set; }
        public string Sector { get; set; }

        // List of events the organization manages
        public ICollection<Event> Events { get; set; }

        public ICollection<OrganizationSocialMediaLink> SocialMediaLinks { get; set; } = new List<OrganizationSocialMediaLink>();

    }

}
