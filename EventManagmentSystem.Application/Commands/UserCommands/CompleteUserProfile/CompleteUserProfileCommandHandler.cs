using AutoMapper;
using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.UserCommands.CompleteUserProfile
{
    public class UpdateUserCommandHandler : IRequestHandler<CompleteUserProfileCommand, Result<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateUserCommandHandler(IMapper mapper, ILogger<UpdateUserCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDto>> Handle(CompleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Find the user by phone number
                var existingUser = await _unitOfWork.UserRepository.GetUserByPhoneNumber(request.PhoneNumber);
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

                await _unitOfWork.UserRepository.UpdateAsync(existingUser);
                await _unitOfWork.SaveAsync();

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
