using AutoMapper;
using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.UserCommands.RegisterUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a user with the same email exists
                var existingUser = await _userRepository.GetByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    // If a user with the provided email already exists, return a failure result
                    return Result.Failure<UserDto>(DomainErrors.User.UserAlreadyExists);
                }

                var socialMediaLinks = new List<UserSocialMediaLink>();

                if (request.SocialMediaLinks != null && request.SocialMediaLinks.Any())
                {
                    socialMediaLinks = request.SocialMediaLinks.Select(linkDto => new UserSocialMediaLink
                    {
                        Platform = linkDto.Platform,
                        Url = linkDto.Url
                    }).ToList();
                }

                // Check if the user with the phone number exists
                var savedUser = await _userRepository.GetUserByPhoneNumber(request.PhoneNumber);

                if (savedUser != null)
                {
                    _logger.LogInformation("Attempt to register with an existing phone number: {PhoneNumber}", request.PhoneNumber);
                    return Result.Failure<UserDto>(DomainErrors.User.PhoneNumberAlreadyExists);
                }

                // Create a new user if none exists with the given phone number
                var newUser = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Name = request.Name,
                    State = request.State,
                    City = request.City,
                    Country = request.Country,
                    SocialMediaLinks = socialMediaLinks
                };

                // Add the new user to the repository
                await _userRepository.CreateUserAsync(newUser);
                savedUser = newUser;

                // Map the saved or newly updated user to UserDto
                var userDto = new UserDto
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
                };
                // Return success with the user details
                return Result.Success(userDto);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserDto>(new Error("UserCreationOrUpdateFailed", ex.Message));
            }
        }
    }
}
