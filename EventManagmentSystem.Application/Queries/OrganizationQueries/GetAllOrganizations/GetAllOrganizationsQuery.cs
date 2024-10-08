using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetAllOrganizations
{
    public class GetAllOrganizationsQuery : IRequest<Result<List<OrganizationDto>>>
    {
    }
}
