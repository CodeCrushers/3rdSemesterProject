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

        [HttpGet]
        public string Get() {
            return null;
        }

        [HttpGet, Route("{id}")]
        public string Get(int id) {
            return null;
        }

        [HttpPost]
        public void Post([FromBody] string value) {
        }
    }
}
