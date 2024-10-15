using AutoMapper;
using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.UserCommands.CompleteUserProfile
{
    public class UpdateUserCommandHandler : IRequestHandler<CompleteUserProfileCommand, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<UserDto>> Handle(CompleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Find the user by phone number
                var existingUser = await _userRepository.GetUserByPhoneNumber(request.PhoneNumber);
                if (existingUser == null)
                {
                    return Result.Failure<UserDto>(DomainErrors.Authentication.UserNotFound);
                }

                existingUser.City = request.City;
                existingUser.Country = request.Country;
                existingUser.State = request.State;
                existingUser.Name = request.Name;
                existingUser.UserName = request.UserName;
                existingUser.Email = request.Email;

                await _userRepository.UpdateAsync(existingUser);

                var userDto = _mapper.Map<UserDto>(existingUser);

                return Result.Success(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing profile for user with phone number {PhoneNumber}", request.PhoneNumber);
                return Result.Failure<UserDto>(new Error("UserUpdateFailed", ex.Message));
            }
        }
    }
}
