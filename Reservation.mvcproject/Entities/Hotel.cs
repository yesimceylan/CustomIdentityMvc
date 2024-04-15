using Reservation.mvcproject.Data;

namespace Reservation.mvcproject.Entities
{
    public class Hotel : BaseEntity
    {
        public string? HotelName { get; set; }
        public string? City{ get; set; }
        public string? Location { get; set; }
        public string? StarRating { get; set; }
        public string? HotelImage { get; set; }
        public string? HotelImage2 { get; set; }
        public string? HotelImage3 { get; set; }
        public string? HotelImage4 { get; set; }
        public string? Price { get; set; }
        public string? PeopleCount { get; set; }
        public string? Bathrooms { get; set; }
        public string? Bedrooms { get; set; }
    }
}
