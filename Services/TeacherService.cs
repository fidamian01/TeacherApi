using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;
using TeacherApi.Services.Interfaces;

namespace TeacherApi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolDbContext _context;

        public TeacherService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task <Teacher> CreateTeacherAsync (Teacher teacher)
        {
            teacher.id = 0;
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<List<Student>> GetStudentsByTeacherIdAsync(int teacherId)
        {
            var teacher = await _context.Teachers.Include(t => t.Classrooms).ThenInclude(c => c.Students).FirstOrDefaultAsync(t => t.Id == teacherId);
            return teacher?.Classrooms.SelectMany(c => c.Students).ToList();
        }
    }
}
