using System;
using System.Collections.Generic;
using System.Linq;
using ExternalClinetSide.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using ExternalClientSide.Models;
using ExternalClientSide;
using 

namespace ExternalClinetSide.BusinessLayer
{
    public class BookingBusinesController
    {
        string baseURL = "https://localhost:44346/";
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

   
        public async void CreateBooking(Booking booking)
        {
            Account account = new Account();
            Car car = new Car();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = new HttpResponseMessage();
                var id = HttpContext.Current.User.Identity.Name;

                res = await client.GetAsync("api/Account/id/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var AccountResponse = res.Content.ReadAsStringAsync().Result;

                    account = JsonConvert.DeserializeObject<Account>(AccountResponse);
                }
            }

            using (var client = new HttpClient)
        }

    }
}