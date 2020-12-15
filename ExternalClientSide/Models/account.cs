using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalClinetSide.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; } 

    }
}