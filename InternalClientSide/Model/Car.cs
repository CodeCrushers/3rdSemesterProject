﻿using InternalClientSide.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalClientSide.Model {
    public class Car {

        public string Brand { get; set; }

        public string Model { get; set; }

        public string RegistrationNumber { get; set; }

        public string LeasingYear { get; set; }

        public int Distance { get; set; }

        public int Charge { get; set; }

        public int Capacity { get; set; }

        public string LocationId { get; set; }

        public bool OnRoute { get; set; }


    }

}
