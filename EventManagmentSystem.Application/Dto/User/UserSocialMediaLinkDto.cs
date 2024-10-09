using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Dto.User
{
    public class UserSocialMediaLinkDto
    {
        public string Id { get; set; }
        public PlatformType Platform { get; set; }
        public string Url { get; set; }
    }
}
