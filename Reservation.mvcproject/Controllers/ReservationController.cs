﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Models.Request;
using Reservation.mvcproject.Entities;
using Serilog;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;

namespace Reservation.mvcproject.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ReservationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CreateReservationIndex(Guid HotelId)
        {
            decimal adultPrice = (decimal)(_dbContext.Hotels.Find(HotelId)?.Price);
            ViewBag.perPersonPrice = adultPrice.ToString();
            return View();
        }
        public IActionResult ReservationDetailIndex()
        {
            return View();
        }

        public IActionResult GetReservationIndex()
        {
            return View();
        }
        public IActionResult GetDetailIndex()
        {
            return View();
        }
        public async Task<IActionResult> PaymentIndex(Guid Id)
        {
            var res = await _dbContext.Reservations.FindAsync(Id);
            return View(res);
        }

        public IActionResult ReservationsIndex()
        {
            //var reservations = _dbContext.Reservations.ToList();
            return View();
        }
        public IActionResult ReservationFilterIndex()
        {
            return View();
        }
        public IActionResult ReservationFilteredIndex()
        {
            return View();
        }

        public async Task<IActionResult> CreateReservation(CreateReservationRequest res)
        {
            var defaultBookingNumberPrefix = "RV";
            Random rnd = new();
            var random = rnd.Next(100000, 999999);
            var resNumber = defaultBookingNumberPrefix + random;

            Res newres = new()
            {
                rezName = res.rezName,
                rezPhoneNumber = res.rezPhoneNumber,
                rezEmail = res.rezEmail,
                rezPerson = res.rezPerson,
                rezChild = res.rezChild,
                rezDescription = res.rezDescription,
                rezDate = res.rezDate,
                rezEndDate = res.rezEndDate,
                HotelId = res.HotelId,
                rezNumber = resNumber
            };
            newres.priceToPay = 0;
            TimeSpan duration = res.rezEndDate - res.rezDate;
            int numberOfDays = duration.Days;
            for (int x = 1; x <= numberOfDays; x++)
            {
                for (int i = 1; i <= res.rezPerson; i++)
                {
                    var hotel = await _dbContext.Hotels.FindAsync(res.HotelId);
                    if (hotel != null)
                    {
                        newres.priceToPay += hotel.Price;
                    }
                }
            }
            for (int x = 1; x <= numberOfDays; x++)
            {
                if (res.rezChild != 0)
                {
                    for (int j = 1; j <= res.rezChild; j++)
                    {
                        var hotel = await _dbContext.Hotels.FindAsync(res.HotelId);
                        if (hotel != null)
                        {
                            newres.priceToPay += ((hotel.Price) / 2);
                        }
                    }
                }
            }

            await _dbContext.Reservations.AddAsync(newres);
            await _dbContext.SaveChangesAsync();
            Log.Information($"{newres.Id} new reservation registration.");
            Log.Information($"{newres.rezNumber} / added new reservation.");
            return RedirectToAction("ReservationDetail", "Reservation", new { resId = newres.Id });
        }

        public async Task<IActionResult> ReservationDetail(Guid resId)
        {
            var res = await _dbContext.Reservations.FindAsync(resId);

            return View("ReservationDetailIndex", res);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(string rezNumber)
        {
            var res = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.rezNumber == rezNumber);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Reservations.Remove(res);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> GetReservation(string rezNumber)
        {
            var res = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.rezNumber == rezNumber);
            if (res == null)
            {
                TempData["ReservationFailed"] = "Reservation ​​not found! Please enter or change a different reservation number.";
                return View("GetReservationIndex");
            }
            Log.Information($"Reservation Number: {rezNumber} to be viewed.");
            return View("GetDetailIndex", res);
        }

        public async Task<IActionResult> GetDetail(string rezNumber)
        {
            var res = await _dbContext.Reservations.FindAsync(rezNumber);

            return View("GetDetailIndex", res);
        }
        [HttpPost]
        public async Task<IActionResult> Payment(Guid Id)
        {
            var res = await _dbContext.Reservations.FindAsync(Id);
            if (res == null)
            {
                return NotFound();
            }

            res.paymentStatus = true;
            await _dbContext.SaveChangesAsync();
            return View("GetDetailIndex", res);
        }
        [HttpPost]
        public async Task<IActionResult> ReservationFilter(ReservationFilterModel model)
        {

            var reservationsWithHotels = await _dbContext.Reservations
    .Where(r => (r.rezDate >= model.rezDate && r.rezDate <= model.rezEndDate) ||
                (r.rezEndDate >= model.rezDate && r.rezEndDate <= model.rezEndDate) ||
                (r.rezDate <= model.rezDate && r.rezEndDate >= model.rezEndDate))
    .Join(
        _dbContext.Hotels,
        reservation => reservation.HotelId,
        hotel => hotel.Id,
        (reservation, hotel) => new { Reservation = reservation, HotelName = hotel.HotelName }
    )
    .ToListAsync();

            if (reservationsWithHotels.Count == 0)
            {
                TempData["NoRes"] = "Reservation not found.";
                return View("ReservationFilterIndex");
            }
            else
            {
                ViewBag.Reservations = reservationsWithHotels;
                return View("ReservationFilteredIndex");
            }

        }
    }
}
