using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagmentSystem.Domain.Models
{
    public class UserSocialMediaLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public PlatformType Platform { get; set; }

        [Required]
        public string Url { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
