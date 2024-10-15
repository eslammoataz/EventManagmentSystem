using EventManagmentSystem.Application;
using EventManagmentSystem.Application.Commands.EventCommands.CreateEvent;
using EventManagmentSystem.Application.Commands.TicketCommands.CreateTicketsForEvent;
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
    private readonly IMediator _mediator;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateEventCommandHandler> logger, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Validate the organization and admin user
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

            // Create the new event
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
                Status = EventStatus.Draft,
                MeetingUrl = request.MeetingUrl,
            };

            // Add the event to the repository
            await _unitOfWork.EventsRepository.AddAsync(newEvent);

            // Process ticket creation
            for (int i = 0; i < request.Tickets.Count; i++)
            {
                var ticketCommand = new CreateTicketsCommand
                {
                    EventId = newEvent.Id,
                    AdminUserId = request.AdminUserId,
                    TypeName = request.Tickets[i].TypeName,
                    Quantity = request.Tickets[i].Quantity,
                    Price = request.Tickets[i].Price,
                };

                var ticketResult = await _mediator.Send(ticketCommand);

                if (ticketResult.IsFailure)
                {
                    _logger.LogError("Failed to create ticket for event ID {EventId}, type {TypeName}. Error: {ErrorDetails}",
                        newEvent.Id,
                        request.Tickets[i].TypeName,
                        ticketResult.Error ?? "Unknown error");

                    // Rollback transaction if ticket creation fails
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result.Failure<EventDto>(DomainErrors.General.UnexpectedError);
                }
            }

            // Save and commit the transaction if everything is successful
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            // Map the new event to an EventDto to return
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

            // Rollback transaction if an exception occurs
            await _unitOfWork.RollbackTransactionAsync();
            return Result.Failure<EventDto>(DomainErrors.General.UnexpectedError);
        }
    }
}
