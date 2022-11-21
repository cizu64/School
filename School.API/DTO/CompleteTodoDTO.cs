using System.ComponentModel.DataAnnotations;

namespace School.API.DTO
{
    public class CompleteTodoDTO
    {
        [Required(ErrorMessage = "Date completed is required")]
        public DateTime DateCompleted { get; set; }
        [Required(ErrorMessage = "Student is required")]
        public int StudentId { get; set; }

    }
}
