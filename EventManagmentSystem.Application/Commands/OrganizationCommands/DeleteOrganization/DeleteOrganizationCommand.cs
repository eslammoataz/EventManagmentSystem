using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.DeleteOrganization
{
    public class DeleteOrganizationCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
