using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;
using TeacherApi.Services.Interfaces;

namespace TeacherApi.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly SchoolDbContext _context;
        public ClassroomService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Classroom> CreateClassroomAsync(Classroom classroom)
        {
            classroom.Id = 0;
            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();
            return classroom;
        }

        public async Task<Classroom> GetClassroomDetailsAsync(int classroomId)
        {
            return await _context.Classrooms.Include(c => c.Students).Include(c => c.Teacher).FirstOrDefaultAsync(c => c.Id == classroomId);

        }
    }
}
