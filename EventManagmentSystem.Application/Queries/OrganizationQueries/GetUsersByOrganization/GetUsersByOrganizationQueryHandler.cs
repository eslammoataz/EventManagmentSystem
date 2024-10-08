using EventManagmentSystem.Application.Dto.User;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetUsersByOrganization
{
    public class GetUsersByOrganizationQueryHandler : IRequestHandler<GetUsersByOrganizationQuery, Result<List<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetUsersByOrganizationQueryHandler> _logger;

        public GetUsersByOrganizationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetUsersByOrganizationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<List<UserDto>>> Handle(GetUsersByOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.OrganizationsRepository.GetByIdAsync(request.OrganizationId);

            if (organization == null)
            {
                _logger.LogWarning("Organization with ID {OrganizationId} not found", request.OrganizationId);
                return Result.Failure<List<UserDto>>(DomainErrors.Organization.OrganizationNotFound);
            }

            var users = organization.Users.Select(user => new UserDto
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            }).ToList();

            return Result.Success(users);
        }
    }
}
