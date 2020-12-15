using System;
using System.Collections.Generic;
using System.Linq;
using ExternalClientSide.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;

namespace ExternalClientSide.BusinessLayer
{
    public class BookingBusinesController
    {
        string baseURL = "https://localhost:44346/";
        public async void CreateBooking(Booking booking)
        {
            Account account = new Account();
            Car car = new Car();
            //getting Account
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = new HttpResponseMessage();
                var id = HttpContext.Current.User.Identity.GetUserId();
                res  = await client.GetAsync("api/Account/id/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var AccountResponse = res.Content.ReadAsStringAsync().Result;

                    account = JsonConvert.DeserializeObject<Account>(AccountResponse);
                }
            }

            // getting car
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = new HttpResponseMessage();
                var CarReg = "789X789";
                res = await client.GetAsync("api/Car/" + CarReg);
                if (res.IsSuccessStatusCode)
                {
                    var CarResponse = res.Content.ReadAsStringAsync().Result;

                    car = JsonConvert.DeserializeObject<Car>(CarResponse);
                }

            }

            booking.Account = account;
            booking.BookingCar = car;

            //post to api booking
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                var json = new JavaScriptSerializer().Serialize(booking);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/Booking", stringContent).Result;

                if(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    Console.WriteLine("it worked");
                }
                else
                {
                    Console.WriteLine("it didnt work");
                }
            }

                Console.WriteLine("test");
        }

    }
}