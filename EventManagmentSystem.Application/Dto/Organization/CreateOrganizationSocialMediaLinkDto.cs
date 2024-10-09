using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Dto.Organization
{
    public class CreateOrganizationSocialMediaLinkDto
    {
        public PlatformType Platform { get; set; }
        public string Url { get; set; }
    }
}
