using EventManagmentSystem.Application;
using EventManagmentSystem.Application.Commands.EventCommands.CreateEvent;
using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<EventDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEventCommandHandler> _logger;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEventCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var organization = await _unitOfWork.OrganizationsRepository.GetByIdAsync(request.OrganizationId);
            if (organization == null)
            {
                _logger.LogWarning("Organization with ID {OrganizationId} not found", request.OrganizationId);
                return Result.Failure<EventDto>(DomainErrors.Organization.OrganizationNotFound);
            }

            if (organization.AdminUserId != request.AdminUserId)
            {
                _logger.LogWarning("User with ID {AdminUserId} is not the admin of organization {OrganizationId}", request.AdminUserId, request.OrganizationId);
                return Result.Failure<EventDto>(DomainErrors.Organization.InvalidAdmin);
            }

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Category = request.Category,
                EventType = request.EventType,
                OrganizerId = request.OrganizationId,
                Organizer = organization,
                ImageUrl = request.ImageUrl,
                VideoUrl = request.VideoUrl,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                Status = EventStatus.Scheduled,
                MeetingUrl = request.MeetingUrl,
            };

            await _unitOfWork.EventsRepository.AddAsync(newEvent);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            var eventDto = new EventDto
            {
                Id = newEvent.Id,
                Title = newEvent.Title,
                Description = newEvent.Description,
                StartDate = newEvent.StartDate,
                EndDate = newEvent.EndDate,
                Category = newEvent.Category.ToString(),
                EventType = newEvent.EventType.ToString(),
                OrganizationId = newEvent.OrganizerId,
                OrganizationName = newEvent.Organizer.Name,
                ImageUrl = newEvent.ImageUrl,
                VideoUrl = newEvent.VideoUrl,
                Longitude = newEvent.Longitude,
                Latitude = newEvent.Latitude,
                Status = newEvent.Status.ToString(),
                MeetingUrl = newEvent.MeetingUrl,
            };

            _logger.LogInformation("Event with ID {EventId} was successfully created", newEvent.Id);
            return Result.Success(eventDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the event with title {Title}", request.Title);
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Failure<EventDto>(DomainErrors.General.UnexpectedError);
        }
    }
}
