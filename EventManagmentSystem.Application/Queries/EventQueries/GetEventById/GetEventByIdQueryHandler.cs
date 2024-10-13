using AutoMapper;
using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.EventQueries.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Result<EventDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventEntity = await _unitOfWork.EventsRepository.GetByIdAsync(request.EventId);
            if (eventEntity == null)
            {
                return Result.Failure<EventDto>(DomainErrors.Event.EventNotFound);
            }


            var eventDto = _mapper.Map<EventDto>(eventEntity);
            return Result.Success(eventDto);
        }
    }
}
