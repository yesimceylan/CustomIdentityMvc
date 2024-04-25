using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reservation.mvcproject.Data;
using Reservation.mvcproject.Models.Request;
using Reservation.mvcproject.Entities;
using Serilog;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult ReservationCompletedIndex()
        {
            return View();
        }
        public IActionResult DeleteReservationIndex()
        {
            return View();
        }

        public async Task<IActionResult> CreateReservation(CreateReservationRequest res)
        {


            Res newres = new()
            {
                rezName = res.rezName,
                rezPhoneNumber = res.rezPhoneNumber,
                rezEmail = res.rezEmail,
                rezPerson = res.rezPerson,
                rezDescription = res.rezDescription,
                rezDate = res.rezDate,
                rezEndDate = res.rezEndDate,
                HotelId = res.HotelId
            };
            await _dbContext.Reservations.AddAsync(newres);
            await _dbContext.SaveChangesAsync();
            Log.Information($"{newres.Id} new reservation registration.");
            return View("ReservationCompletedIndex",newres);
        }
        public async Task<IActionResult> ReservationCompleted()
        {
            var defaultBookingNumberPrefix = "RV";
            Random rnd= new();
            var random = rnd.Next(100000, 999999);
            var resNumber= defaultBookingNumberPrefix + random;
            await _dbContext.AddAsync(resNumber);
            await _dbContext.SaveChangesAsync();
            Log.Information($"{resNumber} / added new reservation.");
            return View();
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
    }
}
