using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTServices.Models;
using RESTServices.Database;
using System.Web.Http.Results;
using System.Web.Http.Description;

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
        public HttpResponseMessage Post(Booking booking) {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict);
            var o = db.Create(booking);
            if (o != null || o is int) {
                response.StatusCode = HttpStatusCode.Created;
            }
            return response;
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
