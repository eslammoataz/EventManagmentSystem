using MediatR;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Dto.User;

namespace EventManagmentSystem.Application.Commands.UserCommands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result<UserDto>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}

