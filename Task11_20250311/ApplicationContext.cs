using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11_20250311
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserSettings> UserSettings { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_User_UserSettings;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<UserSettings>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<User>()
            .HasOne(u => u.UserSettings)   // User has one UserSettings
            .WithOne(us => us.User)        // UserSettings has one User
            .HasForeignKey<UserSettings>(us => us.UserId)  // Foreign Key is in UserSettings
            .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete

            base.OnModelCreating(modelBuilder);
        }
    }
}
