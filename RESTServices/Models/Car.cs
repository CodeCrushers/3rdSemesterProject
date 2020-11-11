using RESTServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.Models {
    public class Car {
        public string Name { get; set; }
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public Manufacturer Manufacturer { get; set; }

    }
}