using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetOrganizationById
{
    public class GetOrganizationByIdQuery : IRequest<Result<OrganizationDto>>
    {
        public string Id { get; set; }
    }
}
