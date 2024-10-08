using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using EventManagmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagmentSystem.Infrastructure.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BulkAddAsync(IEnumerable<Ticket> tickets)
        {
            await _context.Tickets.AddRangeAsync(tickets);
        }

        public async Task<List<Ticket>> GetByIdsAsync(List<string> ticketIds)
        {
            return await _context.Tickets
                                 .Where(t => ticketIds.Contains(t.Id))
                                 .ToListAsync();
        }

        public async Task BulkDeleteAsync(IEnumerable<Ticket> tickets)
        {
            _context.Tickets.RemoveRange(tickets);
        }

        public async Task<Ticket?> GetByIdAsync(string ticketId)
        {
            return await _context.Tickets
                                 .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            return Task.CompletedTask;
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.Tickets
                .Include(t => t.Event)
                .Where(t => t.ApplicationUserId == userId)
                .ToListAsync();
        }
    }
}
