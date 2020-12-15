using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalClinetSide.Models;
using ExternalClinetSide.BusinessLayer;


namespace ExternalClinetSide.Controllers
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
        public ActionResult Booking(Booking bookingInput)
        {
            if(ModelState.IsValid)
            {
                var booking = new Booking { StartLocation = bookingInput.StartLocation, EndLocation = bookingInput.EndLocation, PayedFor = true, BookingDate = DateTime.Now, PaymentAmount = bookingInput.PaymentAmount};
                BookingBController.CreateBooking(booking); 


            }

            return View();
        }
    }
}