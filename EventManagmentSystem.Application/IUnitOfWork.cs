using EventManagmentSystem.Application.Repositories;

namespace EventManagmentSystem.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEventRepository EventsRepository { get; }
        IOrganizationRepository OrganizationsRepository { get; }
        ITicketsRepository TicketsRepository { get; }
        Task<int> SaveAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
