using System.ComponentModel.DataAnnotations;

namespace School.Web.DTO
{
    public class TodoDTO
    {
   
        [Required(ErrorMessage = "Todo name is required")]
        [MaxLength(20, ErrorMessage = "Maximum name required is 20")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Todo description is required")]
        [MaxLength(200, ErrorMessage = "Maximum description required is 200 characters")]
        public string Description { get; set; }
    }
}
