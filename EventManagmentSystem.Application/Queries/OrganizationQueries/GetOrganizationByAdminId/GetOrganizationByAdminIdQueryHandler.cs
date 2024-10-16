using AutoMapper;
using EventManagmentSystem.Application.Dto.Organization;
using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Application.Queries.OrganizationQueries.GetOrganizationByAdminId
{
    public class GetOrganizationByAdminIdQueryHandler : IRequestHandler<GetOrganizationByAdminIdQuery, Result<OrganizationDto>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrganizationByAdminIdQueryHandler> _logger;

        public GetOrganizationByAdminIdQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper, ILogger<GetOrganizationByAdminIdQueryHandler> logger)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<OrganizationDto>> Handle(GetOrganizationByAdminIdQuery request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetByAdminUserIdAsync(request.AdminId);

            if (organization == null)
            {
                _logger.LogWarning($"Organization not found for admin user with id: {request.AdminId}");
                return Result.Failure<OrganizationDto>(DomainErrors.Organization.OrganizationNotFound);
            }

            var organizationDto = _mapper.Map<OrganizationDto>(organization);
            return Result.Success(organizationDto);
        }
    }

}
