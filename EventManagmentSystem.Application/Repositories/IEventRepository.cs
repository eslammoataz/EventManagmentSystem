using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Repositories
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event newEvent);
        Task<Event?> GetByIdAsync(string eventId);
        Task DeleteAsync(Event eventToDelete);
        Task<IEnumerable<Event>> GetAllAsync();

    }
}
