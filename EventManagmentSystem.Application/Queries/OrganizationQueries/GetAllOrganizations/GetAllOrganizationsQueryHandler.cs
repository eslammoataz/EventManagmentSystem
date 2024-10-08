using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetAllOrganizations
{
    public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, Result<List<OrganizationDto>>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetAllOrganizationsQueryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Result<List<OrganizationDto>>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var organizations = await _organizationRepository.GetAllAsync();

            var organizationDtos = organizations.Select(org => new OrganizationDto
            {
                OrganizationId = org.Id,
                OrganizationName = org.Name,
                AdminUserId = org.AdminUserId
            }).ToList();

            return Result.Success(organizationDtos);
        }
    }
}
