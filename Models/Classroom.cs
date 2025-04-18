namespace TeacherApi.Models
{
    public class Classroom
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public List<Student> Students { get; set; } = new();
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Teacher? teacher { get; set; }
    }
}
