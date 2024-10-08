using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.CreateOrganization
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Result<OrganizationDto>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;

        public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<OrganizationDto>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            // Check if the admin user exists
            var adminUser = await _userRepository.GetByIdAsync(request.AdminUserId);
            if (adminUser == null)
            {
                return Result.Failure<OrganizationDto>(DomainErrors.Authentication.UserNotFound);
            }

            var organization = new Organization
            {
                Name = request.OrganizationName,
                AdminUserId = request.AdminUserId
            };

            try
            {
                await _organizationRepository.AddAsync(organization);

                var organizationDto = new OrganizationDto
                {
                    OrganizationId = organization.Id,
                    OrganizationName = organization.Name,
                    AdminUserId = organization.AdminUserId
                };

                return Result.Success(organizationDto);
            }
            catch (Exception ex)
            {
                return Result.Failure<OrganizationDto>(new Error("OrganizationCreationFailed", ex.Message));
            }
        }
    }
}
