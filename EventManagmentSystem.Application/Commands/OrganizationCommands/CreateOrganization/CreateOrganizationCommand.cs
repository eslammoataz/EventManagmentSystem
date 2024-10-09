using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.CreateOrganization
{
    public class CreateOrganizationCommand : IRequest<Result<OrganizationDto>>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ManagerName { get; set; }

        public ICollection<CreateOrganizationSocialMediaLinkDto> SocialMediaLinks { get; set; } = new List<CreateOrganizationSocialMediaLinkDto>();

        public string AdminUserId { get; set; }
    }
}
