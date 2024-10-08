using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.UserQueries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<Result<List<UserDto>>>
    {
    }
}
