using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Repositories
{
    public interface ITicketsRepository
    {
        Task BulkAddAsync(IEnumerable<Ticket> tickets);
        Task<List<Ticket>> GetByIdsAsync(List<string> ticketIds);
        Task BulkDeleteAsync(IEnumerable<Ticket> tickets);

        Task<Ticket> GetByIdAsync(string ticketId);
        Task UpdateAsync(Ticket ticket);

        Task<List<Ticket>> GetTicketsByUserIdAsync(string userId);


    }
}
