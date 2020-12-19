using InternalClientSide.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternalClientSide.Controllers {
    public class CarController {

        private HttpClient HttpClient = new HttpClient();
        private string baseurl = "https://localhost:44346/api/";
        Car Car { get; set; }



        public Car GetCar(string regNumber) {
            string fullUrl = baseurl + "car/" + regNumber + "/";
            Console.WriteLine(fullUrl);
            var response = HttpClient.GetAsync(fullUrl).Result;
            response.EnsureSuccessStatusCode();
            try {
                Car = response.Content.ReadAsAsync<Car>().Result;

            }
            catch (Exception) {

                throw;
            }

            return Car;
        }

    }
}
