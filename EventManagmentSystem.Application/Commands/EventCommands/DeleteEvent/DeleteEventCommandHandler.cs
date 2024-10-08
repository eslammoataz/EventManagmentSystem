using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.EventCommands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteEventCommandHandler> _logger;

        public DeleteEventCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteEventCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var eventEntity = await _unitOfWork.EventsRepository.GetByIdAsync(request.EventId);
                if (eventEntity == null)
                {
                    _logger.LogWarning("Event with ID {EventId} not found", request.EventId);
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result.Failure(DomainErrors.Event.EventNotFound);
                }

                await _unitOfWork.EventsRepository.DeleteAsync(eventEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Event with ID {EventId} was successfully deleted", request.EventId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the event with ID {EventId}", request.EventId);
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Failure(DomainErrors.General.UnexpectedError);
            }
        }
    }
}
