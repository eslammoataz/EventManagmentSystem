using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Dto.Organization
{
    public class OrganizationSocialMediaLinkDto
    {
        public string Id { get; set; }
        public PlatformType Platform { get; set; }
        public string Url { get; set; }

    }
}
