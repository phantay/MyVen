using System.ComponentModel.DataAnnotations;

namespace SEN.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { set; get; }

        public string Password { set; get; }
        public bool Rememberme { set; get; }
    }
}