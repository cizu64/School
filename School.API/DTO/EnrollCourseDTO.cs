using System.ComponentModel.DataAnnotations;

namespace School.API.DTO
{
    public class EnrollCourseDTO
    {
        [Required(ErrorMessage = "Student is required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

    }
}
