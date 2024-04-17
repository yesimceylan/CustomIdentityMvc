 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Entities;
using Reservation.mvcproject.Models.Request;
using Serilog;


namespace santsg.project.Controllers;
[Authorize]
public class HotelController : Controller
{

    private readonly AppDbContext _dbContext;

    public HotelController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IActionResult CreateHotelIndex()
    {
        return View();
    }
    
    public IActionResult UpdateHotelIndex()
    {
        return View();
    }
    
    public IActionResult DeleteHotelIndex()
    {
        return View();
    }
    
    public IActionResult GetHotelIndex()
    {
        var hotels = _dbContext.Hotels.ToList();
        Log.Information("Hotels listed.");
        return View(hotels);
    }

    public IActionResult GetHotelByIdIndex()
    {
        return View();
    }

    public IActionResult SearchHotelIndex()
    {
        return View();
    }
    public IActionResult SearchedHotelIndex()
    {
        return View();
    }
    public IActionResult HotelDetailsIndex(Guid id)
    {
        var hotel = _dbContext.Hotels.FirstOrDefault(x => x.Id == id);
        return View(hotel);
    }

    [HttpGet]
    public async Task<IActionResult> GetHotelById(Guid id)
    {
        var hotel = await _dbContext.Hotels.FindAsync(id);
        if (hotel == null)
        {
            TempData["GetHotelFailed"] = "No hotel with this id was found!";
            return View("GetHotelByIdIndex");
        }
        Log.Information($"HotelId: {id} to be viewed.");
        TempData["Hotel"] = ($"Name: {hotel.HotelName} ");
        TempData["City"] = ($"City: {hotel.City} ");
        TempData["Location"] = ($"Location: {hotel.Location} ");
        TempData["StarRating"] = ($"StarRating: {hotel.StarRating} ");
        TempData["Price"] = ($"Price: {hotel.Price} ");
        return View("GetHotelByIdIndex");
    }

    [HttpPost]
    public async Task<IActionResult> AddHotel(AddHotelRequest hotel)
    {
        Hotel newHotel = new()
        {

            HotelName = hotel.HotelName,
            City = hotel.City,
            Location = hotel.Location,
            StarRating = hotel.StarRating,
            HotelImage = hotel.HotelImage,
            HotelImage2 = hotel.HotelImage2,
            HotelImage3 = hotel.HotelImage3,
            HotelImage4 = hotel.HotelImage4,
            Price = hotel.Price,
            PeopleCount = hotel.PeopleCount,
            Bathrooms = hotel.Bathrooms,
            Bedrooms = hotel.Bedrooms
        };
        await _dbContext.Hotels.AddAsync(newHotel);
        await _dbContext.SaveChangesAsync();
        Log.Information($"HotelName: {newHotel.HotelName}, HotelId: {newHotel.Id} added");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteHotel(Guid id)
    {
        var hotel = await _dbContext.Hotels.FindAsync(id);
        if (hotel == null)
        {
            NotFound();
        }
        _dbContext.Hotels.Remove(hotel);
        await _dbContext.SaveChangesAsync();
        Log.Information($"Hotel Name: {hotel.HotelName} id:{hotel.Id} is deleted.");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateHotel(UpdateHotelRequest hotel)
    {
        var updatedHotel = await _dbContext.Hotels.FindAsync(hotel?.Id);
        if (updatedHotel == null)
        {
            return NotFound();
        }


        updatedHotel.HotelName = hotel?.HotelName;
        updatedHotel.City = hotel?.City;
        updatedHotel.Location = hotel?.Location;
        updatedHotel.StarRating = hotel?.StarRating;
        updatedHotel.HotelImage = hotel?.HotelImage;
        updatedHotel.HotelImage2 = hotel?.HotelImage2;
        updatedHotel.HotelImage3 = hotel?.HotelImage3;
        updatedHotel.HotelImage4 = hotel?.HotelImage4;
        updatedHotel.Price = hotel?.Price;
        updatedHotel.PeopleCount = hotel?.PeopleCount;
        updatedHotel.Bathrooms = hotel?.Bathrooms;
        updatedHotel.Bedrooms = hotel?.Bedrooms;

        _dbContext.Update<Hotel>(updatedHotel);
        await _dbContext.SaveChangesAsync();

        Log.Information($"{updatedHotel} user updated.");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> SearchHotel(string city)
    {
        var hotels = await _dbContext.Hotels.Where(x => x.City.Contains(city)).ToListAsync();
        if (hotels.Count == 0)
        {
            TempData["ReservationFailed"] = "City ​​not found! Please enter or change a different city.";
            return View("SearchHotelIndex");
        }
        else
        {
            ViewBag.Hotels = hotels;
            return View("SearchedHotelIndex");
        }
    }

}
