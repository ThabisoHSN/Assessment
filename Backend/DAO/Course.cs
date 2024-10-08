using System.ComponentModel.DataAnnotations;

namespace Backend.DAO
{
    public class Course
    {
        [Key]
        public Guid GID { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public string? Description { get; set; }
    }
}
