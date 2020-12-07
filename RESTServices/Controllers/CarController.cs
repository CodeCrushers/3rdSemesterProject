using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTServices.Controllers
{
    [RoutePrefix("api/Car")]
    public class CarController : ApiController {

        CarDB db = new CarDB();

        [HttpGet]
        public IEnumerable<Car> Get() {
            return db.GetAll();
        }

        [HttpGet, Route("{id}")]
        public Car Get(int id) {
            return db.Get(id);
        }

        [HttpPost]
        public void Post(Car car) {
            db.Create(car);
        }

        [HttpPut]
        public void Pu(Car car) {
            db.Update(car);
        }

        [HttpDelete, Route("{id}")]
        public void Delete(int id) {
            db.Delete(id);
        }
    }

}
