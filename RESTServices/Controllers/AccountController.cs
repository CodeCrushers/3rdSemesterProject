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

namespace RESTServices.Controllers {

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController {

        private AccountLogic Logic = new AccountLogic();

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Account val) {
            HttpResponseMessage response;
            if (this.Logic.CreateAccount(val)) {
                response = request.CreateResponse(HttpStatusCode.Created);
            } else {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Account>))]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            HttpResponseMessage response;
            IEnumerable<Account> list = this.Logic.GetAllAccounts();
            if(list != null && list.Any()) {
                response = request.CreateResponse(HttpStatusCode.OK, list);
            } else {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpGet, Route("{email}")]
        [ResponseType(typeof(Account))]
        public HttpResponseMessage Get(HttpRequestMessage request, string email) {
            HttpResponseMessage response;
            Account account = this.Logic.GetAccount(email);
            if(account != null) {
                response = request.CreateResponse(HttpStatusCode.OK, account);
            } else {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, Account val) {
            HttpResponseMessage response;
            if(this.Logic.EditAccount(val)) {
                response = request.CreateResponse(HttpStatusCode.NoContent);
            } else {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpDelete, Route("{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id) {
            HttpResponseMessage response;
            if(this.Logic.DeleteAccount(id)) {
                response = request.CreateResponse(HttpStatusCode.Accepted);
            } else {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
