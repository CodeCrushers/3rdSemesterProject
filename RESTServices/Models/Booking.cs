using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace RESTServices.Models {
    public class Booking {

        //public Account Account { get; set; } Implement when done
        public bool PayedFor { get; set; }
        public double PaymentAmount { get; set; }
        //public Location StartLocation { get; set; }
        //public Location EndLocation { get; set; }
        public DateTime BookingDate { get; set; }
        //public Car Car { get; set; }

    }
}