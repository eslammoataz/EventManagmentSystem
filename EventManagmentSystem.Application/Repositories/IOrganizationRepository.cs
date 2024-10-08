using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetByIdAsync(string id);
        Task<Organization> GetByAdminUserIdAsync(string adminUserId);
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
        Task<IEnumerable<Organization>> GetAllAsync();

    }
}
