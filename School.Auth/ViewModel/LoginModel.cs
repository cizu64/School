using System.ComponentModel.DataAnnotations;

namespace School.Auth.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
