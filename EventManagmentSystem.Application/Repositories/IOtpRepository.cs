using EventManagmentSystem.Domain.Models;

namespace EventManagmentSystem.Application.Repositories
{
    public interface IOtpRepository
    {
        Task SaveOtp(Otp otp);
        Task<Otp> GetOtpByPhoneNumber(string phoneNumber);
        Task MarkOtpAsUsed(Otp otp);
    }
}
