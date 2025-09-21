using System.ComponentModel.DataAnnotations;

namespace Voba.WebUI.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Display(Name = "Parola"), Required(ErrorMessage ="Parola boş bırakılamaz!")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
