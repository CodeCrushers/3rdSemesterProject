using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalClientSide.Models;
using ExternalClientSide.BusinessLayer;
using System.Threading.Tasks;
using System.Net;

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
            string redirectController;
            string redirectAction;

            if (ModelState.IsValid)
            {
                var booking = new Booking { StartLocation = bookingInput.StartLocation, EndLocation = bookingInput.EndLocation, PayedFor = true, BookingDate = DateTime.Now, PaymentAmount = bookingInput.PaymentAmount};
                HttpStatusCode HttpStatusCode = await BookingBController.CreateBooking(booking);

                switch(HttpStatusCode)
                {
                    case HttpStatusCode.Created:
                        return RedirectToAction("BookingSucces", "Booking");
                        break;

                    case HttpStatusCode.BadRequest:
                        return RedirectToAction("BookingFail", "Booking");
                        break;
                }
            }
            return RedirectToAction("BookingFail", "Booking");
        }

        public ActionResult BookingSucces()
        {
            return View();
        }

        public ActionResult BookingFail()
        {

            return View();
        }
    }
}