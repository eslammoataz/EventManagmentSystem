using EventManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EventManagmentSystem.Application.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task SaveUserTokenAsync(string userId, string loginProvider, string token);
        Task<IdentityUserToken<string>> GetUserTokenAsync(string userId, string tokenValue);
        Task RemoveUserTokenAsync(IdentityUserToken<string> token);
        Task<ApplicationUser?> GetUserByTokenAsync(string token);



        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByUserNameAsync(string userName);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(ApplicationUser user);

    }
}
