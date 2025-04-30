using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;
using TeacherApi.Services.Interfaces;

namespace TeacherApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent(string name, int classroomId)
        {
            var student = new Student
            {
                Name = name,
                ClassroomId = classroomId
            };
            var created = await _studentService.CreateStudentAsync(student);

            return Ok(new
            {
                message = "Student created successfully.",
                student = new
                {
                    created.Id,
                    created.Name
                }
            });
            
        }

      
        [HttpGet("{id}/teacher-name")]
        public async Task<IActionResult> GetTeacherNameForStudent(int id)
        {
            var teacherName = await _studentService.GetTeacherNameForStudentAsync(id);

            if (teacherName == null)
                return NotFound(new { message = $"No teacher assigned to student with ID {id}." });

            return Ok(new { teacherName });
        }
    }
}
