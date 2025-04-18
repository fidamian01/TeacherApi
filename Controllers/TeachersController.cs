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

    [HttpPost]
    public async Task<IActionResult> CreateTeacher([FromBody] Teacher teacher)
    {
        var createdTeacher = await _teacherService.CreateTeacherAsync(teacher);

        var response = new
        {
            message = "Teacher created successfully",
            teacher = new
            {
                createdTeacher.Id,
            }

        };


        return Ok(response);
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