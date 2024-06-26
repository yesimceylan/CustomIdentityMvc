﻿using System.ComponentModel.DataAnnotations;

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
        [Required]
        [DataType(DataType.MultilineText)]
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        [Compare("PhoneNumber", ErrorMessage ="Phone number don't match.")]
        [Display(Name ="Confirm PhoneNumber")]
        public string? PhoneNumberConfirmed { get; set; }
        
        [Display(Name = "Gender (M/F)")]
        public string? Gender { get; set; }
    }
}
