using EventManagmentSystem.Application.Errors;
using EventManagmentSystem.Application.Helpers;
using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace EventManagmentSystem.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepo _authRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IAuthRepo authRepo,
            IUserRepo userRepo,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _authRepo = authRepo;
            _userRepo = userRepo;
            _logger = logger;
        }

        public async Task<Result<string>> GenerateOtpTokenAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length < 10)
            {
                _logger.LogError("Invalid phone number: {phoneNumber}", phoneNumber);
                return Result.Failure<string>(DomainErrors.Authentication.InvalidPhoneNumber);
            }

            var user = await _userRepo.GetUserByPhoneNumber(phoneNumber);
            if (user == null)
            {
                user = await _userRepo.CreateUserAsync(new ApplicationUser
                {
                    PhoneNumber = phoneNumber,
                    Name = phoneNumber,
                    UserName = phoneNumber,
                });
            }

            var otpCode = await _authRepo.GenerateOtpToken(phoneNumber, user);

            _logger.LogInformation("OTP Code generated successfully for phone number {phoneNumber}", phoneNumber);
            return Result.Success(otpCode);
        }

        public async Task<Result<string>> ValidateOtpTokenAsync(string otpToken, string phoneNumber)
        {
            var user = await _userRepo.GetUserByPhoneNumber(phoneNumber);
            if (user == null)
            {
                _logger.LogError("User with phone number {phoneNumber} not found", phoneNumber);
                return Result.Failure<string>(DomainErrors.Authentication.UserNotFound);
            }

            var isValidOtp = await _authRepo.ValidateOtpToken(otpToken, user);
            if (!isValidOtp)
            {
                _logger.LogError("Invalid OTP token {otpToken} for phone number {phoneNumber}", otpToken, phoneNumber);
                return Result.Failure<string>(DomainErrors.Authentication.InvalidOtp);
            }

            var token = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "LoginToken");

            if (await _userManager.GetAuthenticationTokenAsync(user, "LoginToken", token) == null)
            {
                await _userRepo.SaveUserTokenAsync(user.Id, "LoginToken", token);
            }

            _logger.LogInformation("OTP validated successfully for user {userId}. Token: {token}", user.Id, token);
            return Result.Success(token);
        }

        public async Task<Result<string>> LogoutAsync(string tokenFromHeaders, ClaimsPrincipal userPrincipal)
        {
            if (string.IsNullOrEmpty(tokenFromHeaders))
            {
                _logger.LogError("Token from headers is missing.");
                return Result.Failure<string>(DomainErrors.Authentication.TokenNotFound);
            }

            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                _logger.LogError("User not found.");
                return Result.Failure<string>(DomainErrors.Authentication.UserNotFound);
            }

            var tokenFromDb = await _userRepo.GetUserTokenAsync(user.Id, tokenFromHeaders);
            if (tokenFromDb == null)
            {
                _logger.LogError("Token not found in DB for user {userId}", user.Id);
                return Result.Failure<string>(DomainErrors.Authentication.TokenNotFound);
            }

            await _userRepo.RemoveUserTokenAsync(tokenFromDb);

            _logger.LogInformation("User {userId} logged out successfully", user.Id);
            return Result.Success("User logged out successfully");
        }
    }
}
