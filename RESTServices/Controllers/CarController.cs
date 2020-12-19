using RESTServices.Database;
using RESTServices.LogicLayer;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTServices.Controllers
{
    [RoutePrefix("api/Car")]
    public class CarController : ApiController {

        public CarLogic Logic = new CarLogic();

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Car car) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                if (this.Logic.CreateCar(car)) {
                    response = request.CreateResponse(HttpStatusCode.Created);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Car>))]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                IEnumerable<Car> list = this.Logic.GetAllCars();
                if (list != null && list.Any()) {
                    response = request.CreateResponse(HttpStatusCode.OK, list);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpGet, Route("{reg}")]
        [ResponseType(typeof(Car))]
        public HttpResponseMessage Get(HttpRequestMessage request, string reg) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                Car car = this.Logic.GetCar(reg);
                if (car != null) {
                    response = request.CreateResponse(HttpStatusCode.OK, car);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, Car car) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                if (this.Logic.EditCar(car)) {
                    response = request.CreateResponse(HttpStatusCode.NoContent);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpDelete, Route("{reg}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string reg) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                if (this.Logic.DeleteCar(reg)) {
                    response = request.CreateResponse(HttpStatusCode.Accepted);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }

}
