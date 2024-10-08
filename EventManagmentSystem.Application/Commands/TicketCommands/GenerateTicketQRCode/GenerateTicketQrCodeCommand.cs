using MediatR;

namespace EventManagmentSystem.Application.Commands.TicketCommands.GenerateTicketQRCode
{
    public class GenerateTicketQrCodeCommand : IRequest<byte[]>
    {
        public string TicketId { get; }

        public GenerateTicketQrCodeCommand(string ticketId)
        {
            TicketId = ticketId;
        }
    }
}
