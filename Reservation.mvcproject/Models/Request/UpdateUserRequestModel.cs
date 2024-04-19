using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Reservation.mvcproject.Models.Request
{
    public class UpdateUserRequestModel
    {
        [Required]
        [Column(TypeName="NVARCHAR(100)")]
        public string? Name { get; set; }
        
        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public string? Email { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? Adress { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string? PhoneNumber { get; set; }
    }
}
