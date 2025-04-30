using TeacherApi.Models;

namespace TeacherApi.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<string?> GetTeacherNameForStudentAsync(int studentId);
    }
}
