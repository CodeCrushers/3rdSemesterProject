using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.Models {
    public class Car {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string RegistrationNumber { get; set; }

        public string LeasingYear { get; set; }

        public int Distance { get; set; }

        public int Charge { get; set; }

        public int Capacity { get; set; }
    }
}