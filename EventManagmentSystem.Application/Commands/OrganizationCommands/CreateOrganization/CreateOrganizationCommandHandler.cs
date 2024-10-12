using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUserRepository userRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
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
                Name = request.Name,
                AdminUserId = request.AdminUserId,
                ManagerName = request.ManagerName,
                City = request.City,
                State = request.State,
                Country = request.Country,
                Bio = request.Bio,
                Sector = request.Sector,
            };

            if (request.SocialMediaLinks != null && request.SocialMediaLinks.Any())
            {
                var socialMediaLinks = request.SocialMediaLinks.Select(link => new OrganizationSocialMediaLink
                {
                    Platform = link.Platform,
                    Url = link.Url,
                    Organization = organization
                }).ToList();

                organization.SocialMediaLinks = socialMediaLinks;
            }

            try
            {
                await _organizationRepository.AddAsync(organization);

                var organizationDto = _mapper.Map<OrganizationDto>(organization);


                return Result.Success(organizationDto);
            }
            catch (Exception ex)
            {
                return Result.Failure<OrganizationDto>(new Error("OrganizationCreationFailed", ex.Message));
            }
        }
    }
}
