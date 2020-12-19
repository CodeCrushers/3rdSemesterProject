using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ExternalClientSide.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public bool PayedFor { get; set; }
        public double PaymentAmount { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime BookingDate { get; set; }
        public Car BookingCar { get; set; }
    }
}