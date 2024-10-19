using AutoMapper;
using EventManagmentSystem.Application.Dto.Tickets;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.TicketCommands.SendTicketAsGift
{
    public class SendTicketAsGiftCommandHandler : IRequestHandler<SendTicketAsGiftCommand, Result<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SendTicketAsGiftCommandHandler> _logger;
        private readonly IMapper _mapper;

        public SendTicketAsGiftCommandHandler(IUnitOfWork unitOfWork, ILogger<SendTicketAsGiftCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<TicketDto>> Handle(SendTicketAsGiftCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketsRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {TicketId} not found", request.TicketId);
                return Result.Failure<TicketDto>(DomainErrors.Ticket.TicketNotFound);
            }

            var eventDetails = await _unitOfWork.EventsRepository.GetByIdAsync(ticket.EventId);

            bool isTicketOwner = ticket.ApplicationUserId == request.SenderUserId;
            bool isEventAdmin = eventDetails.Organizer.AdminUserId == request.SenderUserId;

            if (!isTicketOwner && !isEventAdmin)
            {
                _logger.LogWarning("User {SenderUserId} is not authorized to gift the ticket with ID {TicketId}", request.SenderUserId, request.TicketId);
                return Result.Failure<TicketDto>(DomainErrors.Ticket.SenderDoesNotOwnTicket);
            }

            var receiver = await _unitOfWork.UserRepository.GetByIdAsync(request.ReceiverUserId);
            if (receiver == null)
            {
                _logger.LogWarning("Receiver with ID {ReceiverUserId} not found", request.ReceiverUserId);
                return Result.Failure<TicketDto>(DomainErrors.Authentication.UserNotFound);
            }

            ticket.ApplicationUser = receiver;
            ticket.ticketSender = request.SenderUserId;
            ticket.isGift = true;

            await _unitOfWork.SaveAsync();

            var ticketDto = _mapper.Map<TicketDto>(ticket);

            _logger.LogInformation("Ticket with ID {TicketId} successfully gifted from User {SenderUserId} to User {ReceiverUserId}", request.TicketId, request.SenderUserId, request.ReceiverUserId);

            return Result.Success(ticketDto);
        }
    }
}
