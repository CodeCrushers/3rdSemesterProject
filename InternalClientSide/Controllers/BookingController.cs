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
            List<Booking> bookings = null;
            string fullUrl = baseurl + "Booking/Account/" + id + "/";
            try {
                var response = HttpClient.GetAsync(fullUrl).Result;
                response.EnsureSuccessStatusCode();
                bookings = response.Content.ReadAsAsync<List<Booking>>().Result;

            }
            catch (Exception e) {
                
            }

            return bookings;

        }

        public async void ChangeBooking(Booking booking) {
            string fullUrl = baseurl + "Booking/";
            var json = new JavaScriptSerializer().Serialize(booking);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync(fullUrl, stringContent);
        }
    }
}
