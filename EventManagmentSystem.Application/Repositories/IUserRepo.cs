using EventManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EventManagmentSystem.Application.Repositories
{
    public interface IUserRepo
    {
        Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task SaveUserTokenAsync(string userId, string loginProvider, string token);
        Task<IdentityUserToken<string>> GetUserTokenAsync(string userId, string tokenValue);
        Task RemoveUserTokenAsync(IdentityUserToken<string> token);
        Task<ApplicationUser?> GetUserByTokenAsync(string token);
    }
}
