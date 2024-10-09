using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetOrganizationById
{
    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Result<OrganizationDto>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationByIdQueryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Result<OrganizationDto>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetByIdAsync(request.Id);

            if (organization == null)
            {
                return Result.Failure<OrganizationDto>(new Error("OrganizationNotFound", "The organization was not found."));
            }

            var organizationDto = new OrganizationDto
            {
                OrganizationId = organization.Id,
                OrganizationName = organization.Name,
                AdminUserId = organization.AdminUserId,
                State = organization.State,
                Country = organization.Country,
                City = organization.City,
                ManagerName = organization.ManagerName,
                SocialMediaLinks = organization.SocialMediaLinks.Select(link => new OrganizationSocialMediaLinkDto
                {
                    Id = link.Id,
                    Platform = link.Platform,
                    Url = link.Url
                }).ToList()
            };

            return Result.Success(organizationDto);
        }
    }
}
