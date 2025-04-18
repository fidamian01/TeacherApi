using Microsoft.EntityFrameworkCore;
using TeacherApi.Models;

namespace TeacherApi.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>().HasOne(c => c.Teacher).WithMany().HasForeignKey(c => c.TeacherId).IsRequired(false);
        }
    }
}
