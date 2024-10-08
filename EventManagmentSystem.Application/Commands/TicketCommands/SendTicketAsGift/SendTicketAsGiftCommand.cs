using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.SendTicketAsGift
{
    public class SendTicketAsGiftCommand : IRequest<Result<TicketDto>>
    {
        public string SenderUserId { get; }
        public string ReceiverUserId { get; }
        public string TicketId { get; }

        public SendTicketAsGiftCommand(string senderUserId, string receiverUserId, string ticketId)
        {
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
            TicketId = ticketId;
        }
    }

    public class SendTicketAsGiftRequest
    {
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string TicketId { get; set; }
    }
}
