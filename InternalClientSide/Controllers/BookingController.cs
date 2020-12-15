using InternalClientSide.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InternalClientSide.Controllers {
    class BookingController {

        private HttpClient HttpClient = new HttpClient();
        private string baseurl = "https://localhost:44346/api/";
        public Booking Booking { get; set; }


        public List<Booking> GetBookings(string id) {
            List<Booking> bookings;
            string fullUrl = baseurl + "Booking/Account/" + id + "/";
            var response = HttpClient.GetAsync(fullUrl).Result;
            response.EnsureSuccessStatusCode();
            try {
                bookings = response.Content.ReadAsAsync<List<Booking>>().Result;

            }
            catch (Exception) {

                throw;
            }

            return bookings;

        }

        public void ChangeBooking(int id) {
            string fullUrl = baseurl + "account/";
            var json = new JavaScriptSerializer().Serialize(Booking);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = HttpClient.PutAsync(fullUrl, stringContent);
        }
    }
}
