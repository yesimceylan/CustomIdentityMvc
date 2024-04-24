namespace Reservation.mvcproject.Models.Request
{
    public class AddHotelRequest
    {
        public int Id { get; set; }
        public string? HotelName { get; set; }
        public string? City { get; set; }
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
        public bool Wifi { get; set; }
        public bool Shower { get; set; }
        public bool TV { get; set; }
        public bool Kitchen { get; set; }
        public bool Heating { get; set; }
        public bool Accessible { get; set; }
        public int? CheckIn { get; set; }
        public int? CheckOut { get; set; }
    }
}
