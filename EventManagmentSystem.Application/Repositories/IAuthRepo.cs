using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Repositories
{
    public interface IAuthRepo
    {
        Task<string> GenerateOtpToken(string phoneNumber, ApplicationUser user);
        Task<bool> ValidateOtpToken(string otpToken, ApplicationUser user);
    }
}
