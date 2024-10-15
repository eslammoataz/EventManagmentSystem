using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagmentSystem.Domain.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string EventId { get; set; }
        public Event Event { get; set; }  // Navigation property

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }  // Navigation property

        // Use the enum for TicketType
        public string Type { get; set; }  // Ticket type (VIP, Regular, Student, etc.)

        public decimal Price { get; set; }
        public bool IsCheckedIn { get; set; } = false;  // Check-in status

        public bool isGift { get; set; }

        public string? ticketSender { get; set; }

    }

    public enum TicketType
    {
        VIP,
        Regular,
        Student
    }

}
