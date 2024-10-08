using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.RemoveUsersFromOrganization
{
    public class RemoveUsersFromOrganizationCommand : IRequest<Result>
    {
        public string OrganizationId { get; set; }
        public string AdminUserId { get; set; }
        public List<string> UserIds { get; set; }

        public RemoveUsersFromOrganizationCommand(string organizationId, string adminUserId, List<string> userIds)
        {
            OrganizationId = organizationId;
            AdminUserId = adminUserId;
            UserIds = userIds;
        }
    }

    public class RemoveUsersFromOrganizationRequest
    {
        public string AdminUserId { get; set; }
        public List<string> UserIds { get; set; }
    }
}
