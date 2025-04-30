using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherApi.Data;
using TeacherApi.Models;
using TeacherApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTeacher(string name)
    {
        var teacher = new Teacher
        {
            Name = name
        };

        var createdTeacher = await _teacherService.CreateTeacherAsync(teacher);

        return Ok(new
        {
            message = "Teacher created successfully.",
            teacher = new
            {
                createdTeacher.Id,
                createdTeacher.Name
            }
        });
    }


    [HttpGet("{teacherId}/students")]
    public async Task<IActionResult> GetStudentsByTeacher(int teacherId)
    {
        var result = await _teacherService.GetStudentsByTeacherIdAsync(teacherId);
        if (result == null)
            return NotFound(new { message = $"Teacher with Id{teacherId} not found." });

        return Ok(result);
    }

}
