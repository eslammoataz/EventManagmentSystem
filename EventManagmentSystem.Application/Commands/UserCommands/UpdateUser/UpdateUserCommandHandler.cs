using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.UserCommands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Fetch the user by ID
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", request.Id);
                return Result.Failure<UserDto>(DomainErrors.Authentication.UserNotFound);
            }

            // Update user properties
            user.Name = request.Name;
            user.City = request.City;
            user.State = request.State;
            user.Country = request.Country;
            user.PhoneNumber = request.PhoneNumber;

            // Save changes
            await _unitOfWork.SaveAsync();

            // Map to DTO
            var userDto = new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                State = user.State,
                Country = user.Country,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name
            };

            _logger.LogInformation("User with ID {UserId} successfully updated", request.Id);
            return Result.Success(userDto);
        }
    }
}
