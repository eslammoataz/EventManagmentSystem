using EventManagmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagmentSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Otp> Otps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Organization and AdminUser
            modelBuilder.Entity<Organization>()
                .HasOne(o => o.AdminUser)
                .WithMany()  // AdminUser may not have a collection of organizations
                .HasForeignKey(o => o.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict); // Set appropriate delete behavior


            // Configure many-to-many relationship between ApplicationUser and Organization
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Organizations)
                .WithMany(o => o.Users)
                .UsingEntity(j => j.ToTable("UserOrganizations"));

            // Configure enum to be stored as string in the database
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Event>()
                .Property(e => e.Category)
                .HasConversion<string>();

        }
    }
}
