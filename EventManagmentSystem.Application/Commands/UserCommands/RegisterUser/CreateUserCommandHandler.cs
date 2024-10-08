using AutoMapper;
using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using MediatR;

namespace EventManagmentSystem.Application.Commands.UserCommands.RegisterUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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


                // Check if the user with the phone number exists
                var savedUser = await _userRepository.GetUserByPhoneNumber(request.PhoneNumber);

                if (savedUser != null)
                {
                    // Update the existing user's details
                    savedUser.UserName = request.UserName;
                    savedUser.Email = request.Email;
                    savedUser.Name = request.Name;

                    // Update the user in the repository
                    await _userRepository.UpdateAsync(savedUser);
                }
                else
                {
                    // Create a new user if none exists with the given phone number
                    var newUser = new ApplicationUser
                    {
                        UserName = request.UserName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        Name = request.Name
                    };

                    // Add the new user to the repository
                    await _userRepository.CreateUserAsync(newUser);
                    savedUser = newUser;  // Assign the newly created user to savedUser
                }

                // Map the saved or newly updated user to UserDto
                var userDto = _mapper.Map<UserDto>(savedUser);

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
