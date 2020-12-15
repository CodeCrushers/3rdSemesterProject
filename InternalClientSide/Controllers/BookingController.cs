using InternalClientSide.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InternalClientSide.Controllers {
    class BookingController {

        private HttpClient HttpClient = new HttpClient();
        private string baseurl = "https://localhost:44346/api/";


        public List<Booking> GetBookings(string id) {

            return new List<Booking>() {
                new Booking {
                    Account = new Account() {
                        Id = "testID",
                        Name = "Bob",
                        Phone = "10000000",
                        Email = "Test@Mail.com"
                    },
                    PayedFor = true,
                    PaymentAmount = 100,
                    StartLocation = "Hobrovej 3",
                    EndLocation = "Danmarksgade 4",
                    BookingDate = new DateTime(2020, 9, 12).Date,
                    BookingCar = new Car() {
                        Brand = "Tesla",
                        Model = "Model S",
                        RegistrationNumber = "123X123",
                        LeasingYear = "2020",
                        Distance = 1000,
                        Charge = 100,
                        Capacity = 5,
                        OnRoute = false,
                        LocationId = "Kildevældet 3"

                    }
                }
            };

        }
    }
}
