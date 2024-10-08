using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using QRCoder;

namespace EventManagmentSystem.Application.Commands.TicketCommands.BookTicket
{
    public class BookTicketCommandHandler : IRequestHandler<BookTicketCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookTicketCommandHandler> _logger;

        public BookTicketCommandHandler(IUnitOfWork unitOfWork, ILogger<BookTicketCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(BookTicketCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found.", request.UserId);
                    return Result.Failure(DomainErrors.Authentication.UserNotFound);
                }


                // Fetch the event by its ID
                var eventItem = await _unitOfWork.EventsRepository.GetByIdAsync(request.EventId);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event with ID {EventId} not found.", request.EventId);
                    return Result.Failure(DomainErrors.Event.EventNotFound);
                }

                // Find the first available (unbooked) ticket for the event
                var availableTicket = eventItem.Tickets.FirstOrDefault(t => t.ApplicationUserId == null && t.Type == request.TicketType);
                if (availableTicket == null)
                {
                    _logger.LogWarning("No available tickets found for event {EventId}.", request.EventId);
                    return Result.Failure(DomainErrors.Ticket.NoAvailableTickets);
                }

                availableTicket.ApplicationUserId = request.UserId;

                await _unitOfWork.TicketsRepository.UpdateAsync(availableTicket);

                await _unitOfWork.SaveAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Ticket with ID {TicketId} was successfully booked for user {UserId}.", availableTicket.Id, request.UserId);
                return Result.Success();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while booking the ticket for event {EventId} and user {UserId}.", request.EventId, request.UserId);

                // Rollback the transaction in case of failure
                await _unitOfWork.RollbackTransactionAsync();

                return Result.Failure(DomainErrors.General.UnexpectedError);
            }
        }


        private string GenerateQRCode(string ticketId)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(ticketId, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);

                using (var qrCodeImage = qrCode.GetGraphic(20))
                {
                    using (var stream = new MemoryStream())
                    {
                        qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        var qrCodeImageBytes = stream.ToArray();

                        // Convert the QR code image to base64 string (optional)
                        string qrCodeBase64 = Convert.ToBase64String(qrCodeImageBytes);
                        string qrCodeUrl = $"data:image/png;base64,{qrCodeBase64}";

                        // Alternatively, you could store it as an image in a cloud or server and return a URL.
                        return qrCodeUrl;
                    }
                }
            }

        }
    }
}
