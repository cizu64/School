using System.ComponentModel.DataAnnotations;

namespace School.API.DTO
{
    public class EnrollCourseDTO
    {
        
        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

    }
}
