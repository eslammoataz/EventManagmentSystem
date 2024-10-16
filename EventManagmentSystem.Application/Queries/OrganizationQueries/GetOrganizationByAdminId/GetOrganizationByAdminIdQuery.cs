using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetOrganizationByAdminId
{
    public class GetOrganizationByAdminIdQuery : IRequest<Result<OrganizationDto>>
    {
        public string AdminId { get; set; }

    }
    public class GetOrganizationByAdminIdRequest
    {
        public string AdminId { get; set; }
    }
}
