using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18_20250328
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Instructor> Instructors { get; set; } = null!;
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
                .UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=March_StudentsCoursesSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set required props
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.FamilyName).IsRequired();
            modelBuilder.Entity<Instructor>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Instructor>().Property(i => i.FamilyName).IsRequired();
            modelBuilder.Entity<Course>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Course>().Property(c => c.Description).IsRequired();

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

            // Course - Instructor relations
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor) // Each Course must have one Instructor
                .WithMany(i => i.Courses) // A course can have many enrollments
                .HasForeignKey(c => c.InstructorId) // Foreign key to Instructor
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
