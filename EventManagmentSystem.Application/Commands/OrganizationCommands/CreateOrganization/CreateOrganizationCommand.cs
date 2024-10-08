using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.CreateOrganization
{
    public class CreateOrganizationCommand : IRequest<Result<OrganizationDto>>
    {
        public string OrganizationName { get; set; }
        public string AdminUserId { get; set; }
    }
}
