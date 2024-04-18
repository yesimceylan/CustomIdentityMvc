using System.ComponentModel.DataAnnotations;

namespace Reservation.mvcproject.Models.Request
{
    public class UpdateUserRequestModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
