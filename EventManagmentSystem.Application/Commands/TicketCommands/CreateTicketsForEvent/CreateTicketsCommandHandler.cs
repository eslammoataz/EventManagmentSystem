using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.TicketCommands.CreateTicketsForEvent
{
    public class CreateTicketsCommandHandler : IRequestHandler<CreateTicketsCommand, Result<List<TicketDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTicketsCommandHandler> _logger;

        public CreateTicketsCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateTicketsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<List<TicketDto>>> Handle(CreateTicketsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the event by ID
                var eventItem = await _unitOfWork.EventsRepository.GetByIdAsync(request.EventId);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event with ID {EventId} not found", request.EventId);
                    return Result.Failure<List<TicketDto>>(DomainErrors.Event.EventNotFound);
                }

                // Check if the requester is the admin of the organization that owns the event
                if (eventItem.Organizer.AdminUserId != request.AdminUserId)
                {
                    _logger.LogWarning("User with ID {AdminUserId} is not authorized to create tickets for event {EventId}", request.AdminUserId, request.EventId);
                    return Result.Failure<List<TicketDto>>(DomainErrors.User.UserNotAuthorized);
                }

                var tickets = new List<Ticket>();
                for (int i = 0; i < request.Quantity; i++)
                {
                    var ticket = new Ticket
                    {
                        EventId = request.EventId,
                        Type = request.TypeName,
                        Price = request.Price,
                        ApplicationUserId = null,
                        IsCheckedIn = false
                    };
                    tickets.Add(ticket);
                }

                // Add tickets in bulk
                await _unitOfWork.TicketsRepository.BulkAddAsync(tickets);

                // Save changes to the database
                await _unitOfWork.SaveAsync();

                // Map created tickets to TicketDto
                var ticketDtos = tickets.Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    EventId = ticket.EventId,
                    Type = ticket.Type,
                    Price = ticket.Price,
                    IsCheckedIn = ticket.IsCheckedIn,
                    ApplicationUserId = ticket.ApplicationUserId
                }).ToList();

                _logger.LogInformation("Created {Quantity} tickets for event {EventId}", request.Quantity, request.EventId);
                return Result.Success(ticketDtos);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating tickets for event {EventId}", request.EventId);
                return Result.Failure<List<TicketDto>>(DomainErrors.General.UnexpectedError);
            }
        }
    }
}
