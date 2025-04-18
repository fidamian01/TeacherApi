using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;

namespace TeacherApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public ClassroomsController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassroom([FromBody] Classroom classroom)
        {
            classroom.Id = 0;

            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();

            var response = new
            {
                message = "Classroom created successfully.",
                classroom = new
                {
                    classroom.Id,
                    classroom.Name
                }
            };

            return Ok(response);
        }

        [HttpGet("{classroomId}/details")]
        public async Task<IActionResult> GetClassroomDetails(int classroomId)
        {
            var classroom = await _context.Classrooms
                .Include(c => c.Students)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == classroomId);

            if (classroom == null)
                return NotFound(new { message = $"Classroom with ID {classroomId} not found." });

            return Ok(new
            {
                classroomId = classroom.Id,
                classroomName = classroom.Name,
                teacher = classroom.Teacher != null ? new
                {
                    classroom.Teacher.Id,
                    classroom.Teacher.Name
                } : null,
                students = classroom.Students.Select(s => new
                {
                    s.Id,
                    s.Name
                }).ToList()
            });
        }
    }

}
