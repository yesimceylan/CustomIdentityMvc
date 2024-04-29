using System.ComponentModel.DataAnnotations.Schema;

namespace Reservation.mvcproject.Entities
{
    public class Res : BaseEntity
    {
     
        public string? rezName { get; set; }
        public string? rezPhoneNumber { get; set; }
        public string? rezEmail { get; set; }
        public int? rezPerson { get; set; }
        public int? rezChild {  get; set; }
        public string? rezDescription { get; set; }
        public DateTime? rezDate { get; set; }
        public DateTime? rezEndDate { get; set; }
        public DateTime? rezCreateDate { get; set; } = DateTime.Now;

        public Guid HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
        public string? rezNumber { get; set; }
    }
}
