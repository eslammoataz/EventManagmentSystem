using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagmentSystem.Domain.Models
{
    public class Otp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }          // Primary Key
        public string PhoneNumber { get; set; } // Phone number associated with the OTP
        public string Code { get; set; }       // The OTP code
        public DateTime Expiration { get; set; } // Expiration time for the OTP
        public bool IsUsed { get; set; }      // Flag to indicate if OTP has been used
        public bool IsExpired() => DateTime.UtcNow > Expiration;
    }
}
