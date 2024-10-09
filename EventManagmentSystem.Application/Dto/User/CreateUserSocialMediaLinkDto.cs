using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Dto.User
{
    public class CreateUserSocialMediaLinkDto
    {
        public PlatformType Platform { get; set; }
        public string Url { get; set; }
    }
}
