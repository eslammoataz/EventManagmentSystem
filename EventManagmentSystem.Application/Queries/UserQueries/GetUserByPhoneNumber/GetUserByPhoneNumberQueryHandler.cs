using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;

namespace EventManagmentSystem.Application.Queries.UserQueries.GetUserByPhoneNumber
{
    public class GetUserByPhoneNumberQueryHandler : IRequestHandler<GetUserByPhoneNumberQuery, Result<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByPhoneNumberQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDto>> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByPhoneNumber(request.PhoneNumber);

            if (user == null)
            {
                return Result.Failure<UserDto>(DomainErrors.Authentication.UserNotFound);
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
