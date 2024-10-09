using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagmentSystem.Domain.Models
{
    public class OrganizationSocialMediaLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public PlatformType Platform { get; set; }

        [Required]
        public string Url { get; set; }

        public string OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }

    public enum PlatformType
    {
        Facebook,
        X,
        Instagram,
        LinkedIn,
        YouTube,
        TikTok,
        Snapchat,
        Other
    }
}
