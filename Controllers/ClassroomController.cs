using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;
using TeacherApi.Services.Interfaces;

namespace TeacherApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClassroom(string name, int? teacherId = null)
        {
            var classroom = new Classroom
            {
                Name = name,
                TeacherId = (int)teacherId
            };

            var created = await _classroomService.CreateClassroomAsync(classroom);

            return Ok(new
            {
                message = "Classroom created successfully.",
                classroom = new
                {
                    created.Id,
                    created.Name
                }
            });
        }


        [HttpGet("{classroomId}/details")]
        public async Task<IActionResult> GetClassroomDetails(int classroomId)
        {
            var classroom = await _classroomService.GetClassroomDetailsAsync(classroomId);

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
