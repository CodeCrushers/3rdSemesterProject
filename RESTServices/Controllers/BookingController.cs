using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTServices.Models;
using RESTServices.Database;
using System.Web.Http.Results;

namespace RESTServices.Controllers {

    [RoutePrefix("api/Booking")]
    public class BookingController : ApiController {

        public BookingDB db = new BookingDB();

        [HttpGet]
        public IEnumerable<Booking> Get() {
            return db.GetAll();
        }

        [HttpGet, Route("{id}")]
        public Booking Get(int id) {
            return db.Get(id);
        }

        [HttpPost]
        public void Post(Booking booking) {
            db.Create(booking);
        }

        [HttpPut]
        public void Put(Booking booking) {
            db.Update(booking);
        }

        [HttpDelete, Route("{id}")]
        public void Delete(int id) {
            db.Delete(id);
        }
    }
}
