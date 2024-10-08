using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.TicketCommands.DeleteTickets
{
    public class DeleteTicketsCommandHandler : IRequestHandler<DeleteTicketsCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTicketsCommandHandler> _logger;

        public DeleteTicketsCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteTicketsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTicketsCommand request, CancellationToken cancellationToken)
        {
            // Start a transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Fetch the tickets by their IDs
                var tickets = await _unitOfWork.TicketsRepository.GetByIdsAsync(request.TicketIds);

                if (tickets == null || tickets.Count == 0)
                {
                    _logger.LogWarning("No tickets found for the provided IDs.");
                    return Result.Failure(DomainErrors.Ticket.TicketsNotFound);
                }

                // Delete tickets in bulk
                await _unitOfWork.TicketsRepository.BulkDeleteAsync(tickets);

                // Save the changes
                await _unitOfWork.SaveAsync();

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Deleted {Count} tickets.", tickets.Count);
                return Result.Success();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting tickets.");

                // Rollback the transaction in case of an error
                await _unitOfWork.RollbackTransactionAsync();

                return Result.Failure(DomainErrors.Ticket.DeletionFailed);
            }
        }
    }
}
