using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Models.Request;
using Reservation.mvcproject.Entities;
using Serilog;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Serialization;

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
            return View();
        }
        public IActionResult ReservationDetailIndex()
        {
            return View();
        }
        public IActionResult DeleteReservationIndex()
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
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            var res = await _dbContext.Reservations.FindAsync(id);
            if (res == null)
            {
                NotFound();
            }
            _dbContext.Reservations.Remove(res);
            await _dbContext.SaveChangesAsync();
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
    }
}
