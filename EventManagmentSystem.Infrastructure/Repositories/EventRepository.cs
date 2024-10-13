using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using EventManagmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagmentSystem.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> AddAsync(Event newEvent)
        {
            var createdEvent = await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return createdEvent.Entity;
        }

        public async Task<Event?> GetByIdAsync(string eventId)
        {
            return await _context.Events
                                 .Include(e => e.Organizer)
                                 .Include(e => e.Tickets)
                                 .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task DeleteAsync(Event eventToDelete)
        {
            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
                                 .Include(e => e.Organizer)
                                 .Include(e => e.Tickets)
                                 .ToListAsync();
        }
    }
}
