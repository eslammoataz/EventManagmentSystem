using AutoMapper;
using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Queries.TicketQueries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Result<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTicketByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetTicketByIdQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<TicketDto>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketsRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {TicketId} not found", request.TicketId);
                return Result.Failure<TicketDto>(DomainErrors.Ticket.TicketNotFound);
            }

            var ticketDto = _mapper.Map<TicketDto>(ticket);

            _logger.LogInformation("Ticket with ID {TicketId} successfully retrieved", request.TicketId);

            return Result.Success(ticketDto);
        }
    }
}
