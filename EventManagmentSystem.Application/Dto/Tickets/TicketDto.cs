namespace EventManagmentSystem.Application.Dto.Tickets
{
    public class TicketDto
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool IsCheckedIn { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? TicketSenderUserId { get; set; }
        public bool isGift { get; set; } = false;
    }
}
