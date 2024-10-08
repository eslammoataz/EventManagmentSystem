using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using MediatR;

namespace EventManagmentSystem.Application.Commands.OrganizationCommands.DeleteOrganization
{
    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, Result>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<Result> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetByIdAsync(request.Id);

            if (organization == null)
            {
                return Result.Failure(new Error("OrganizationNotFound", "The organization was not found."));
            }

            await _organizationRepository.DeleteAsync(organization);

            return Result.Success();
        }
    }
}
