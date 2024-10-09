namespace EventManagmentSystem.Application.Dto.Organization
{
    public class CreateOrganizationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ManagerName { get; set; }

        // Social media links
        public ICollection<OrganizationSocialMediaLinkDto> SocialMediaLinks { get; set; } = new List<OrganizationSocialMediaLinkDto>();
    }
}
