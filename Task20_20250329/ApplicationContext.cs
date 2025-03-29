using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task20_20250329
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Train> Trains { get; set; } = null!;
        public DbSet<Station> Stations { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_TrainStationSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set required props
            modelBuilder.Entity<Station>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Train>().Property(t => t.Number).IsRequired();
            modelBuilder.Entity<Train>().Property(t => t.Model).IsRequired();

            // Station - Train relations
            modelBuilder.Entity<Train>()
                .HasOne(t => t.Station) // Each Train must have one Station
                .WithMany(s => s.Trains) // A station can have many trains
                .HasForeignKey(e => e.StationId) // Foreign key to Station
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
