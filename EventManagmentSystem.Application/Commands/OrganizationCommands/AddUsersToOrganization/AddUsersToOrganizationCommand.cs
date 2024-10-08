using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.AddUsersToOrganization
{
    public class AddUsersToOrganizationCommand : IRequest<Result>
    {
        public string OrganizationId { get; set; }
        public string AdminUserId { get; set; }
        public List<string> UserIds { get; set; }

        public AddUsersToOrganizationCommand(string organizationId, string adminUserId, List<string> userIds)
        {
            OrganizationId = organizationId;
            AdminUserId = adminUserId;
            UserIds = userIds;
        }
    }

    public class AddUsersToOrganizationRequest
    {
        public string AdminUserId { get; set; }
        public List<string> UserIds { get; set; }
    }
}
