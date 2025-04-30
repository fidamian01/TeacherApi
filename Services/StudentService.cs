using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;
using TeacherApi.Services.Interfaces;

namespace TeacherApi.Services
{
    public class StudentService : IStudentService
    {
        public readonly SchoolDbContext _context;
        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Student> CreateStudentAsync(Student student)
        {
            student.Id = 0;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<string?> GetTeacherNameForStudentAsync(int studentId)
        {
            Student student = await _context.Students.Include(s => s.Classroom).ThenInclude(c => c.Teacher).FirstOrDefaultAsync(s => s.Id == studentId);
            return student?.Classroom?.Teacher?.Name;
        }
    }
}
