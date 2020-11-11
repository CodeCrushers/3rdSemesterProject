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
    [RoutePrefix("api/car")]
    public class CarController : ApiController {

        /*
        private CarDB _carDB = new CarDB();

        public void Create(Car entity) {
            _carDB.Create(entity);
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Car entity) {
            throw new NotImplementedException();
        }

        Car ICRUD<Car>.Get(int id) {
            throw new NotImplementedException();
        }*/

        [HttpGet]
        public string Get() {
            return "Hello World";
        }

        [HttpGet, Route("{name}")]
        public string Get(string name) {
            string helloString = "Hello " + name;
            return helloString;
        }

        [HttpGet, Route("{id}")]
        public string Get(int id) {
            return null;
        }

    }

}
