using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.UserQueries.GetUserByPhoneNumber
{
    public class GetUserByPhoneNumberQuery : IRequest<Result<UserDto>>
    {
        public string PhoneNumber { get; set; }

        public GetUserByPhoneNumberQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
