 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Entities;
using Reservation.mvcproject.Models.Request;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;


namespace Reservation.mvcproject.Controllers;
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
    
    //public IActionResult DeleteHotelIndex()
    //{
    //    return View();
    //}
    
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
    public IActionResult ErrorIndex()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> GetHotelById(Guid id)
    {
        var hotel = await _dbContext.Hotels.FindAsync(id);
        if (hotel == null)
        {
            //TempData["GetHotelFailed"] = "No hotel with this id was found!";
            return View("ErrorIndex","Hotel");
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
            Bedrooms = hotel.Bedrooms,
            Wifi = hotel.Wifi,
            Shower = hotel.Shower,
            TV = hotel.TV,
            Kitchen= hotel.Kitchen,
            Heating= hotel.Heating,
            Accessible= hotel.Accessible,
            CheckIn= hotel.CheckIn,
            CheckOut= hotel.CheckOut
        };
        await _dbContext.Hotels.AddAsync(newHotel);
        await _dbContext.SaveChangesAsync();
        Log.Information($"HotelName: {newHotel.HotelName}, HotelId: {newHotel.Id} added");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteHotel(Guid id)
    {
        //var hotel = await _dbContext.Hotels.FindAsync(id);
        //if (hotel == null)
        //{
        //    NotFound();
        //}
        //_dbContext.Hotels.Remove(hotel);
        //await _dbContext.SaveChangesAsync();
        //Log.Information($"Hotel Name: {hotel.HotelName} id:{hotel.Id} is deleted.");
        //return RedirectToAction("Index", "Home");
        var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(u => u.Id == id);
        if (hotel == null)
        {
            Log.Information("Hotel not found during deletion!");
            return NotFound();
        }
        else
        {
            _dbContext.Hotels.Remove(hotel);
            await _dbContext.SaveChangesAsync();

            Log.Information($"{hotel.HotelName} user deleted.");
        }
        return RedirectToAction("GetHotelIndex", "Hotel");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateHotel(UpdateHotelRequest hotel)
    {
        var updatedHotel = await _dbContext.Hotels.FindAsync(hotel?.Id);
        if (updatedHotel == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(hotel.HotelName))
        {
            updatedHotel.HotelName = hotel.HotelName;
        }

        if (!string.IsNullOrEmpty(hotel.City))
        {
            updatedHotel.City = hotel.City;
        }

        if (!string.IsNullOrEmpty(hotel.Location))
        {
            updatedHotel.Location = hotel.Location;
        }

        if (hotel.StarRating != null)
        {
            updatedHotel.StarRating = hotel.StarRating;
        }

        if (hotel.HotelImage != null)
        {
            updatedHotel.HotelImage = hotel.HotelImage;
        }

        if (hotel.HotelImage2 != null)
        {
            updatedHotel.HotelImage2 = hotel.HotelImage2;
        }

        if (hotel.HotelImage3 != null)
        {
            updatedHotel.HotelImage3 = hotel.HotelImage3;
        }

        if (hotel.HotelImage4 != null)
        {
            updatedHotel.HotelImage4 = hotel.HotelImage4;
        }

        if (hotel.Price != null)
        {
            updatedHotel.Price = hotel.Price;
        }

        if (hotel.PeopleCount != null)
        {
            updatedHotel.PeopleCount = hotel.PeopleCount;
        }

        if (hotel.Bathrooms != null)
        {
            updatedHotel.Bathrooms = hotel.Bathrooms;
        }

        if (hotel.Bedrooms != null)
        {
            updatedHotel.Bedrooms = hotel.Bedrooms;
        }
        if (hotel.Wifi != null)
        {
            updatedHotel.Wifi = hotel.Wifi;
        }
        if (hotel.Shower != null)
        {
            updatedHotel.Shower = hotel.Shower;
        }
        if (hotel.TV != null)
        {
            updatedHotel.TV = hotel.TV;
        }
        if (hotel.Kitchen != null)
        {
            updatedHotel.Kitchen = hotel.Kitchen;
        }
        if (hotel.Heating != null)
        {
            updatedHotel.Heating = hotel.Heating;
        }
        if (hotel.Accessible != null)
        {
            updatedHotel.Accessible = hotel.Accessible;
        }
        if (hotel.CheckIn != null)
        {
            updatedHotel.CheckIn = hotel.CheckIn;
        }
        if (hotel.CheckOut != null)
        {
            updatedHotel.CheckOut = hotel.CheckOut;
        }
        _dbContext.Update(updatedHotel);
        await _dbContext.SaveChangesAsync();

        Log.Information($"{updatedHotel} user updated.");
        return RedirectToAction("GetHotelIndex", "Hotel");
    }


    [HttpPost]
    public async Task<IActionResult> SearchHotel(string city)
    {
        var hotels = await _dbContext.Hotels.Where(x => x.City.Contains(city)).ToListAsync();
        var cities = _dbContext.Hotels.Select(c => c.City).Distinct().ToList();
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

    public async Task<IActionResult> Error()
    {
        return View();
    }
}
