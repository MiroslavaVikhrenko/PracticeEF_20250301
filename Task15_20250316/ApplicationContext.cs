using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task15_20250316
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<GuestEvent> GuestEvents { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // switch on lazy loading
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_EventsForGuests;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set Name as required
            modelBuilder.Entity<Guest>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Event>().Property(e => e.Name).IsRequired();

            // Correct Check Constraint for Role
            modelBuilder.Entity<GuestEvent>()
                .ToTable(t => t.HasCheckConstraint("Role", $"Role IN ({(int)Role.Speaker}, {(int)Role.Manager}, {(int)Role.Visitor})"));

            // Many-to-Many Relationship (with explicit join table GuestEvent)
            modelBuilder.Entity<GuestEvent>()
                .HasKey(ge => ge.Id); // Primary Key for GuestEvent

            modelBuilder.Entity<GuestEvent>()
                .HasOne(ge => ge.Guest) // Each GuestEvent must have one Guest
                .WithMany(g => g.GuestEvents) // A Guest can have multiple GuestEvent entries
                .HasForeignKey(ge => ge.GuestId) // Foreign key to Guest
                .OnDelete(DeleteBehavior.Cascade); // Deleting a GuestEvent does not delete Guest

            modelBuilder.Entity<GuestEvent>()
                .HasOne(ge => ge.Event) // Each GuestEvent must have one Event
                .WithMany(e => e.GuestEvents)// An Event can have multiple GuestEvent entries
                .HasForeignKey(ge => ge.EventId) // Foreign key to Event
                .OnDelete(DeleteBehavior.Cascade); // Deleting a GuestEvent does not delete Event


            base.OnModelCreating(modelBuilder);
        }
    }
}
