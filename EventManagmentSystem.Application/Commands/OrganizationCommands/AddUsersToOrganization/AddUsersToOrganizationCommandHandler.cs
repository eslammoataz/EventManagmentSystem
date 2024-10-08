using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.AddUsersToOrganization
{
    public class AddUsersToOrganizationCommandHandler : IRequestHandler<AddUsersToOrganizationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddUsersToOrganizationCommandHandler> _logger;

        public AddUsersToOrganizationCommandHandler(IUnitOfWork unitOfWork, ILogger<AddUsersToOrganizationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(AddUsersToOrganizationCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Fetch the organization
                var organization = await _unitOfWork.OrganizationsRepository.GetByIdAsync(request.OrganizationId);
                if (organization == null)
                {
                    _logger.LogWarning("Organization with ID {OrganizationId} not found", request.OrganizationId);
                    return Result.Failure(DomainErrors.Organization.OrganizationNotFound);
                }

                // Validate that the requester is the admin
                if (organization.AdminUserId != request.AdminUserId)
                {
                    _logger.LogWarning("User with ID {AdminUserId} is not the admin of organization {OrganizationId}", request.AdminUserId, request.OrganizationId);
                    return Result.Failure(DomainErrors.Organization.InvalidAdmin);
                }

                // Add users to organization
                var usersAdded = new List<string>();
                foreach (var userId in request.UserIds)
                {
                    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                    if (user == null)
                    {
                        _logger.LogWarning("User with ID {UserId} not found", userId);
                        continue; // Skip if user not found
                    }

                    if (!organization.Users.Contains(user))
                    {
                        organization.Users.Add(user);
                        usersAdded.Add(userId);
                    }
                    else
                    {
                        _logger.LogInformation("User with ID {UserId} is already a member of organization {OrganizationId}", userId, request.OrganizationId);
                    }
                }

                // Save changes to the database
                if (usersAdded.Count > 0)
                {
                    await _unitOfWork.SaveAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    _logger.LogInformation("Users {UserIds} were successfully added to organization {OrganizationId}", string.Join(",", usersAdded), request.OrganizationId);
                    return Result.Success();
                }

                return Result.Failure(DomainErrors.Organization.NoUsersAdded);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding users to organization {OrganizationId}", request.OrganizationId);
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Failure(DomainErrors.General.UnexpectedError);
            }
        }
    }
}
