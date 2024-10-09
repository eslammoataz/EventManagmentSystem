using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.UserCommands.RegisterUser
{
    public class CreateUserCommand : IRequest<Result<UserDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public ICollection<CreateOrganizationSocialMediaLinkDto> SocialMediaLinks { get; set; } = new List<CreateOrganizationSocialMediaLinkDto>();

    }
}
