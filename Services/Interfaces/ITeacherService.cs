using TeacherApi.Models;

namespace TeacherApi.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> CreateTeacherAsync(Teacher teacher);
        Task<List<Student>> GetStudentsByTeacherIdAsync(int teacherId);
    }
}
