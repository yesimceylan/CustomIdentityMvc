using Microsoft.Build.Framework;
using Reservation.mvcproject.Entities;

namespace Reservation.mvcproject.Models.Request
{
    public class ReservationFilterModel
    {
        public Hotel Hotel { get; set; }
        [Required]
        public DateTime? rezDate { get; set; }
        [Required]
        public DateTime? rezEndDate { get; set; }
    }
}
