using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventManagmentSystem.Infrastructure.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthRepo> _logger;

        public AuthRepo(UserManager<ApplicationUser> userManager, ILogger<AuthRepo> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        // Generate an OTP token using UserManager and return it
        public async Task<string> GenerateOtpToken(string phoneNumber, ApplicationUser user)
        {
            var otpToken = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "OtpToken");
            _logger.LogInformation("Generated OTP for user {userId} with phone {phoneNumber}", user.Id, phoneNumber);
            return otpToken;
        }

        // Validate an OTP token using UserManager
        public async Task<bool> ValidateOtpToken(string otpToken, ApplicationUser user)
        {
            var isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultPhoneProvider, "OtpToken", otpToken);
            _logger.LogInformation("OTP validation for user {userId} returned {isValid}", user.Id, isValid);
            return isValid;
        }
    }
}
