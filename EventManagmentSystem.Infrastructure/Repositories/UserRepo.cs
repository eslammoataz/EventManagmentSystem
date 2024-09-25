using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using EventManagmentSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagmentSystem.Infrastructure.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepo(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Retrieve a user by their phone number
        public async Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        // Create a new user with a given phone number
        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        // Save a generated token to the database
        public async Task SaveUserTokenAsync(string userId, string loginProvider, string token)
        {
            _context.UserTokens.Add(new IdentityUserToken<string>
            {
                UserId = userId,
                LoginProvider = loginProvider,
                Name = "AuthToken",
                Value = token
            });
            await _context.SaveChangesAsync();
        }

        // Retrieve a user token by userId and token value
        public async Task<IdentityUserToken<string>> GetUserTokenAsync(string userId, string tokenValue)
        {
            return await _context.UserTokens
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Value == tokenValue);
        }

        // Remove a user token from the database
        public async Task RemoveUserTokenAsync(IdentityUserToken<string> token)
        {
            _context.UserTokens.Remove(token);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetUserByTokenAsync(string token)
        {
            // Find the user token in AspNetUserTokens table
            var userToken = await _context.UserTokens
                .FirstOrDefaultAsync(t => t.Value == token);

            if (userToken == null)
            {
                // If no token is found, return null
                return null;
            }

            // Retrieve the user associated with the token
            var user = await _userManager.FindByIdAsync(userToken.UserId);

            return user;
        }

    }
}
