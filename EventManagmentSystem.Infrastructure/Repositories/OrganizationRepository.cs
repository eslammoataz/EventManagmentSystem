using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using EventManagmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagmentSystem.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Organization?> GetByIdAsync(string id)
        {
            return await _context.Organizations
                                 .Include(o => o.AdminUser)
                                 .Include(o => o.Users)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Organization?> GetByAdminUserIdAsync(string adminUserId)
        {
            return await _context.Organizations
                                 .FirstOrDefaultAsync(o => o.AdminUserId == adminUserId);
        }

        public async Task AddAsync(Organization organization)
        {
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization)
        {
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }
    }
}
