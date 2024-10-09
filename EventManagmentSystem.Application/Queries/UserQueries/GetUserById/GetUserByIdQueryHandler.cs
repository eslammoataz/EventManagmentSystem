using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;

namespace EventManagmentSystem.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return Result.Failure<UserDto>(new Error("UserNotFound", "The user was not found."));
            }

            var userDto = new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Country = user.Country,
                State = user.State,
                City = user.City,
                SocialMediaLinks = user.SocialMediaLinks.Select(link => new UserSocialMediaLinkDto
                {
                    Id = link.Id,
                    Platform = link.Platform,
                    Url = link.Url
                }).ToList()
            };

            return Result.Success(userDto);
        }
    }
}
