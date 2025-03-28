using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task19_20250328
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies() // switch on lazy loading
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_UniversityMgmtSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set required props
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Course>().Property(c => c.Title).IsRequired();
            modelBuilder.Entity<Enrollment>().Property(e => e.Grade).IsRequired();

            // Enrollemnt - Students relations
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student) // Each Enrollment must have one Student
                .WithMany(s => s.Enrollments) // A student can have many enrollments
                .HasForeignKey(e => e.StudentId) // Foreign key to Student
                .OnDelete(DeleteBehavior.Cascade);

            // Enrollment - Course relations
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course) // Each Enrollment must have one Course
                .WithMany(c => c.Enrollments) // A course can have many enrollments
                .HasForeignKey(e => e.CourseId) // Foreign key to Course
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
