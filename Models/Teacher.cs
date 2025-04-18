using System.Text.Json.Serialization;

namespace TeacherApi.Models
{
    public class Teacher
    {
        internal int id;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Classroom> Classrooms { get; set; }
    }

}
