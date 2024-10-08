using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Queries.TicketQueries.GetTicketsByEvent
{
    public class GetTicketsByEventQueryHandler : IRequestHandler<GetTicketsByEventQuery, Result<List<TicketDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTicketsByEventQueryHandler> _logger;

        public GetTicketsByEventQueryHandler(IUnitOfWork unitOfWork, ILogger<GetTicketsByEventQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<List<TicketDto>>> Handle(GetTicketsByEventQuery request, CancellationToken cancellationToken)
        {
            // Fetch the event by ID
            var eventItem = await _unitOfWork.EventsRepository.GetByIdAsync(request.EventId);
            if (eventItem == null)
            {
                _logger.LogWarning("Event with ID {EventId} not found", request.EventId);
                return Result.Failure<List<TicketDto>>(DomainErrors.Event.EventNotFound);
            }

            // Retrieve tickets for the event
            var tickets = eventItem.Tickets;

            if (tickets == null || !tickets.Any())
            {
                _logger.LogInformation("No tickets found for event {EventId}", request.EventId);
                return Result.Success(new List<TicketDto>());
            }

            // Map tickets to TicketDto
            var ticketDtos = tickets.Select(ticket => new TicketDto
            {
                Id = ticket.Id,
                EventId = ticket.EventId,
                Type = ticket.Type.ToString(),
                Price = ticket.Price,
                IsCheckedIn = ticket.IsCheckedIn,
                ApplicationUserId = ticket.ApplicationUserId
            }).ToList();

            return Result.Success(ticketDtos);
        }
    }

}
