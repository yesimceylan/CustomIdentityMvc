using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Reservation.mvcproject.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public string? Gender { get; set; }
    }
}
