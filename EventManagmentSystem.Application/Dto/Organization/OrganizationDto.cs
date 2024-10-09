namespace EventManagmentSystem.Application.Dto.Organization
{
    public class OrganizationDto
    {
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string AdminUserId { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string ManagerName { get; set; }
        public string City { get; set; }

        public ICollection<OrganizationSocialMediaLinkDto> SocialMediaLinks { get; set; } = new List<OrganizationSocialMediaLinkDto>();
    }
}
