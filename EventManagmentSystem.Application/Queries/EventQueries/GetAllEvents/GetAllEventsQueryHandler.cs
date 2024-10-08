using AutoMapper;
using EventManagmentSystem.Application.Dto.Events;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.EventQueries.GetAllEvents
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, Result<IEnumerable<EventDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var eventEntities = await _unitOfWork.EventsRepository.GetAllAsync();
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(eventEntities);
            return Result.Success(eventDtos);
        }
    }
}
