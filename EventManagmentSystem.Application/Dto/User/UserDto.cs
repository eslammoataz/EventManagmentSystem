namespace EventManagmentSystem.Application.Dto.User
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public ICollection<UserSocialMediaLinkDto> SocialMediaLinks { get; set; } = new List<UserSocialMediaLinkDto>();
    }
}
