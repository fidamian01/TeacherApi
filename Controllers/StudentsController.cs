using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;

namespace TeacherApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            student.Id = 0;

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var response = new
            {
                message = "Student created successfully.",
                student = new
                {
                    student.Id,
                    student.Name
                }
            };

            return Ok(response);
        }

      
        [HttpGet("{id}/teacher-name")]
        public async Task<IActionResult> GetTeacherNameForStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Classroom)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound(new { message = $"Student with ID {id} not found." });

            if (student.Classroom?.Teacher == null)
                return NotFound(new { message = $"No teacher assigned to student with ID {id}." });

            return Ok(new
            {
                teacherName = student.Classroom.Teacher.Name
            });
        }
    }
}
