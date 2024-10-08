using MediatR;
using Microsoft.Extensions.Logging;
using QRCoder;
using System.Drawing.Imaging;

namespace EventManagmentSystem.Application.Commands.TicketCommands.GenerateTicketQRCode
{
    public class GenerateTicketQrCodeCommandHandler : IRequestHandler<GenerateTicketQrCodeCommand, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenerateTicketQrCodeCommandHandler> _logger;

        public GenerateTicketQrCodeCommandHandler(IUnitOfWork unitOfWork, ILogger<GenerateTicketQrCodeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<byte[]> Handle(GenerateTicketQrCodeCommand request, CancellationToken cancellationToken)
        {
            // Fetch the ticket from the database using the repository
            var ticket = await _unitOfWork.TicketsRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {TicketId} not found", request.TicketId);
                return null;
            }

            // Generate the QR code for the ticket
            return GenerateQRCodeImage(ticket.Id);
        }

        private byte[] GenerateQRCodeImage(string ticketId)
        {
            // Replace "https://yourdomain.com" with your actual domain or API URL
            string checkInUrl = $" https://7908-156-193-198-166.ngrok-free.app/api/tickets/checkin/{ticketId}";

            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(checkInUrl, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                using (var qrCodeImage = qrCode.GetGraphic(20))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        qrCodeImage.Save(memoryStream, ImageFormat.Png);
                        return memoryStream.ToArray();
                    }
                }
            }
        }


        //private byte[] GenerateQRCodeImage(string ticketId)
        //{
        //    using (var qrGenerator = new QRCodeGenerator())
        //    {
        //        var qrCodeData = qrGenerator.CreateQrCode(ticketId, QRCodeGenerator.ECCLevel.Q);
        //        var qrCode = new QRCode(qrCodeData);

        //        using (var qrCodeImage = qrCode.GetGraphic(20))  // Adjust size if needed
        //        {
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                qrCodeImage.Save(memoryStream, ImageFormat.Png);  // Save as PNG image
        //                return memoryStream.ToArray();  // Return the image as a byte array
        //            }
        //        }
        //    }
        //}
    }
}
