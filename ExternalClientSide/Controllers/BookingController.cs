using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalClientSide.Models;
using ExternalClientSide.BusinessLayer;
using System.Threading.Tasks;

namespace ExternalClientSide.Controllers
{
    public class BookingController : Controller
    {
        BookingBusinesController BookingBController = new BookingBusinesController();
        // GET: Booking
        public ActionResult BookingMap()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Booking(Booking bookingInput)
        {
            if(ModelState.IsValid)
            {
                var booking = new Booking { StartLocation = bookingInput.StartLocation, EndLocation = bookingInput.EndLocation, PayedFor = true, BookingDate = DateTime.Now, PaymentAmount = bookingInput.PaymentAmount};
                if (await BookingBController.CreateBooking(booking) == true)
                {
                    return RedirectToAction("BookingSucces", "Booking");
                }
                    return RedirectToAction("Login", "Account");
                


            }
            return RedirectToAction("Login", "Account");

            
        }

        public ActionResult BookingSucces()
        {
            return View();
        }
    }
}