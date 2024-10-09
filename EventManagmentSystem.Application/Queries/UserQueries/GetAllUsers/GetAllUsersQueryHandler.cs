using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;

namespace EventManagmentSystem.Application.Queries.UserQueries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = users.Select(savedUser => new UserDto
            {
                UserId = savedUser.Id,
                UserName = savedUser.UserName,
                Name = savedUser.Name,
                Email = savedUser.Email,
                PhoneNumber = savedUser.PhoneNumber,
                Country = savedUser.Country,
                State = savedUser.State,
                City = savedUser.City,
                SocialMediaLinks = savedUser.SocialMediaLinks.Select(link => new UserSocialMediaLinkDto
                {
                    Id = link.Id,
                    Platform = link.Platform,
                    Url = link.Url
                }).ToList()
            }).ToList();


            return Result.Success(userDtos);
        }
    }
}
