using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Dto.User;

namespace EventManagmentSystem.Application.Dto.CompleteProfileAndOrganization
{
    public class CompleteProfileAndOrganizationDto
    {
        public CreateUserDto User { get; set; }
        public CreateOrganizationDto Organization { get; set; }
    }
}
