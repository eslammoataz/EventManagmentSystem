using EventManagmentSystem.Application.Helpers;
using System.Security.Claims;

namespace EventManagmentSystem.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<string>> GenerateOtpTokenAsync(string phoneNumber);
        Task<Result<string>> ValidateOtpTokenAsync(string otpToken, string phoneNumber);
        Task<Result<string>> LogoutAsync(string tokenFromHeaders, ClaimsPrincipal user);


    }
}
