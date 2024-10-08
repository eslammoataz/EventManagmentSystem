using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public string Id { get; set; }
    }
}
