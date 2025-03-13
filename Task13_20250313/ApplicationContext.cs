using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task13_20250313
{
    /*
     Описать классы: «Страна», «Аэропорт», «Самолет», «Характеристики самолета». 
    Реализовать возможность получение полных данных, а самолете 
    (сам самолет, его характеристики, аэропорт в котором он находится, 
    и страна в которой находится аэропорт). Задачу можно реализовать, 
    используя методы Include / ThenInclude или Lazy Loading.
     */
    public class ApplicationContext : DbContext
    {
        public DbSet<Aircraft> Aircrafts { get; set; } = null!;
        public DbSet<AircraftSpecs> AircraftSpecs { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // switch on lazy loading
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_Avia;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<Airport>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<Country>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<Aircraft>()
                .HasOne(a => a.Airport) // aircraft can be in only one airport
                .WithMany(ap => ap.Aircrafts) // airport can have several aircrafts
                .HasForeignKey(a => a.AirportId) // Foreign key for Aircraft is AirportId
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Airport>()
                .HasOne(ap => ap.Country) // airport can have only one country
                .WithMany(c => c.Airports) // country can have several airports
                .HasForeignKey(ap  => ap.CountryId) // Airport has foreign key CountryId
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AircraftSpecs>()
                .HasOne(asp => asp.Aircraft) // aircraft specs has only one aircraft
                .WithOne(a => a.AircraftSpecs) // aircraft has only one aircraft specs
                //.HasForeignKey(asp => asp.AircraftId) // aircraft specs has foreign key AircraftId
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
