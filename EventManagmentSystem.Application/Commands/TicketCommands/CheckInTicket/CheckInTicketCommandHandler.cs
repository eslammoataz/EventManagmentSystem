using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.TicketCommands.CheckInTicket
{
    public class CheckInTicketCommandHandler : IRequestHandler<CheckInTicketCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckInTicketCommandHandler> _logger;

        public CheckInTicketCommandHandler(IUnitOfWork unitOfWork, ILogger<CheckInTicketCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(CheckInTicketCommand request, CancellationToken cancellationToken)
        {
            // Fetch the ticket from the database using the TicketId
            var ticket = await _unitOfWork.TicketsRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {TicketId} not found", request.TicketId);
                return Result.Failure(DomainErrors.Ticket.TicketNotFound);
            }

            // Check if the ticket is already checked in
            if (ticket.IsCheckedIn)
            {
                _logger.LogInformation("Ticket with ID {TicketId} is already checked in", request.TicketId);
                return Result.Failure(DomainErrors.Ticket.AlreadyCheckedIn);
            }

            // Mark the ticket as checked in
            ticket.IsCheckedIn = true;
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Ticket with ID {TicketId} was successfully checked in", request.TicketId);
            return Result.Success();
        }
    }
}
