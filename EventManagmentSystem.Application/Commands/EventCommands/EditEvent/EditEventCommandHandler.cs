using AutoMapper;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.EventCommands.EditEvent
{
    public class EditEventCommandHandler : IRequestHandler<EditEventCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EditEventCommandHandler> _logger;

        public EditEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EditEventCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> Handle(EditEventCommand request, CancellationToken cancellationToken)
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

                _mapper.Map(request, eventEntity);

                if (request.StartDate.HasValue)
                {
                    eventEntity.StartDate = request.StartDate.Value;
                }

                if (request.EndDate.HasValue)
                {
                    eventEntity.EndDate = request.EndDate.Value;
                }

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Event with ID {EventId} was successfully updated", request.EventId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the event with ID {EventId}", request.EventId);
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Failure(DomainErrors.General.UnexpectedError);
            }
        }
    }
}
