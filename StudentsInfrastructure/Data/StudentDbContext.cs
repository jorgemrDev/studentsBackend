using Microsoft.EntityFrameworkCore;
using StudentDomain.Entities;

namespace StudentsInfrastructure.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<SubjectAssignment> SubjectAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.StudentId, e.SubjectId })
                .IsUnique();

            modelBuilder.Entity<SubjectAssignment>()
                .HasIndex(sa => new { sa.ProfessorId, sa.SubjectId })
                .IsUnique();
        }
    }
}
