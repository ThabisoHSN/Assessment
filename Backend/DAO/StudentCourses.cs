using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DAO
{

    public class StudentCourses
    {
        [Key]
        public Guid GID { get; set; }
        public Guid StudentGID { get; set; }
        public Guid CourseGID { get; set; }
        public DateTime? RegisteredDate { get; set; }

        [ForeignKey("StudentGID")]
        public Student? Student { get; set; }
        [ForeignKey("CourseGID")]
        public Course? Course { get; set; }
    }
}
