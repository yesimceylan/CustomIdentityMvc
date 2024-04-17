using System.ComponentModel.DataAnnotations;

namespace Reservation.mvcproject.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password don't match.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        //multiline adress icin bir kac satır ayırır
        [DataType(DataType.MultilineText)]
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
