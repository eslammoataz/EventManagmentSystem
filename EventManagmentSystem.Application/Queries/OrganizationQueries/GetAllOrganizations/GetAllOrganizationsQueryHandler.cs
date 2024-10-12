using AutoMapper;
using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetAllOrganizations
{
    public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, Result<List<OrganizationDto>>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public GetAllOrganizationsQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OrganizationDto>>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var organizations = await _organizationRepository.GetAllAsync();

            var organizationDtos = _mapper.Map<List<OrganizationDto>>(organizations);

            return Result.Success(organizationDtos);
        }
    }
}
