using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.RemoveUsersFromOrganization
{
    public class RemoveUsersFromOrganizationCommandHandler : IRequestHandler<RemoveUsersFromOrganizationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveUsersFromOrganizationCommandHandler> _logger;

        public RemoveUsersFromOrganizationCommandHandler(IUnitOfWork unitOfWork, ILogger<RemoveUsersFromOrganizationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(RemoveUsersFromOrganizationCommand request, CancellationToken cancellationToken)
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

                // Remove users from organization
                var usersRemoved = new List<string>();
                foreach (var userId in request.UserIds)
                {
                    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                    if (user == null)
                    {
                        _logger.LogWarning("User with ID {UserId} not found", userId);
                        continue; // Skip if user not found
                    }

                    if (organization.Users.Contains(user))
                    {
                        organization.Users.Remove(user);
                        usersRemoved.Add(userId.ToString());
                    }
                    else
                    {
                        _logger.LogInformation("User with ID {UserId} is not a member of organization {OrganizationId}", userId, request.OrganizationId);
                    }
                }

                // Save changes to the database
                if (usersRemoved.Count > 0)
                {
                    await _unitOfWork.SaveAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    _logger.LogInformation("Users {UserIds} were successfully removed from organization {OrganizationId}", string.Join(",", usersRemoved), request.OrganizationId);
                    return Result.Success();
                }

                return Result.Failure(DomainErrors.Organization.NoUsersRemoved);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing users from organization {OrganizationId}", request.OrganizationId);
                await _unitOfWork.RollbackTransactionAsync();
                return Result.Failure(DomainErrors.General.UnexpectedError);
            }
        }
    }
}
