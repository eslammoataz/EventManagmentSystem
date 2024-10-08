using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetUsersByOrganization
{
    public class GetUsersByOrganizationQuery : IRequest<Result<List<UserDto>>>
    {
        public string OrganizationId { get; set; }
    }
}
