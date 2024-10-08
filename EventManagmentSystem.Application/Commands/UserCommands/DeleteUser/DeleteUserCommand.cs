using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Commands.UserCommands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
