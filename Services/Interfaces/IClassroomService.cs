using TeacherApi.Models;

namespace TeacherApi.Services.Interfaces
{
    public interface IClassroomService
    {
        Task<Classroom> CreateClassroomAsync(Classroom classroom);
        Task<Classroom> GetClassroomDetailsAsync(int classroomId);
    }
}
