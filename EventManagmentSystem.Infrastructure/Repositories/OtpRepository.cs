using EventManagmentSystem.Application.Repositories;
using EventManagmentSystem.Domain.Models;
using EventManagmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagmentSystem.Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly ApplicationDbContext _context;
        public OtpRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Otp> GetOtpByPhoneNumber(string phoneNumber)
        {
            return await _context.Otps
                .Where(o => o.PhoneNumber == phoneNumber && !o.IsUsed && o.Expiration > DateTime.UtcNow)
                .OrderByDescending(o => o.Expiration)
                .FirstOrDefaultAsync();
        }

        public async Task MarkOtpAsUsed(Otp otp)
        {
            otp.IsUsed = true;
            _context.Otps.Update(otp);
            await _context.SaveChangesAsync();
        }

        public async Task SaveOtp(Otp otp)
        {
            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();
        }
    }
}
