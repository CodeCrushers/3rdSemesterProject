﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTServices.Models;
using RESTServices.Database;
using System.Web.Http.Results;
using System.Web.Http.Description;
using RESTServices.LogicLayer;

namespace RESTServices.Controllers {

    [RoutePrefix("api/Booking")]
    public class BookingController : ApiController {

        public BookingLogic Logic = new BookingLogic();

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Booking booking) {
            HttpResponseMessage response;
            if (this.Logic.CreateBooking(booking)) {
                response = request.CreateResponse(HttpStatusCode.Created);
            } else {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Booking>))]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            HttpResponseMessage response;
            IEnumerable<Booking> list = this.Logic.GetAllBookings();
            if (list != null && list.Any()) {
                response = request.CreateResponse(HttpStatusCode.OK, list);
            } else {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpGet, Route("{id}")]
        [ResponseType(typeof(Booking))]
        public HttpResponseMessage Get(HttpRequestMessage request, string id) {
            HttpResponseMessage response;
            Booking booking = this.Logic.GetBooking(id);
            if (booking != null) {
                response = request.CreateResponse(HttpStatusCode.OK, booking);
            } else {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, Booking booking) {
            HttpResponseMessage response;
            if (this.Logic.EditBooking(booking)) {
                response = request.CreateResponse(HttpStatusCode.NoContent);
            } else {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpDelete, Route("{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id) {
            HttpResponseMessage response;
            if (this.Logic.DeleteBooking(id)) {
                response = request.CreateResponse(HttpStatusCode.Accepted);
            } else {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
