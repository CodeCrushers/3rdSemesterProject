using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalClinetSide.Models;

namespace ExternalClinetSide.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult BookingMap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Booking(Booking bookingInput)
        {
            if(ModelState.IsValid)
            {
                var booking = new Booking { StartLocation = bookingInput.StartLocation, EndLocation = bookingInput.EndLocation, Account = null, PayedFor = true, BookingCar = null, BookingDate = new DateTime(), PaymentAmount = bookingInput.PaymentAmount};



            }

            return View();
        }
        public ActionResult BookingTest()
        {
            return View();
        }
    }
}