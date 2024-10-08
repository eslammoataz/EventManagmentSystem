using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Queries.TicketQueries.GetUserTickets
{
    public class GetUserTicketsQueryHandler : IRequestHandler<GetUserTicketsQuery, Result<List<TicketDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetUserTicketsQueryHandler> _logger;

        public GetUserTicketsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetUserTicketsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<List<TicketDto>>> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken)
        {
            // Fetch all tickets that are assigned to the user
            var tickets = await _unitOfWork.TicketsRepository.GetTicketsByUserIdAsync(request.UserId);

            if (tickets == null || !tickets.Any())
            {
                _logger.LogInformation("No tickets found for user with ID {UserId}.", request.UserId);
                return Result.Failure<List<TicketDto>>(DomainErrors.Ticket.NoTicketsForUser);
            }

            // Map the tickets to TicketDto
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
