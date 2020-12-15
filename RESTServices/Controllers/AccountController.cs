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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                if (this.Logic.CreateAccount(val)) {
                    response = request.CreateResponse(HttpStatusCode.Created);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Account>))]
        public HttpResponseMessage Get(HttpRequestMessage request) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                IEnumerable<Account> list = this.Logic.GetAllAccounts();
                if(list != null && list.Any()) {
                    response = request.CreateResponse(HttpStatusCode.OK, list);
                } 
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpGet, Route("id/{id}")]
        [ResponseType(typeof(Account))]
        public HttpResponseMessage GetAccountById(HttpRequestMessage request, string id) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                Account account = this.Logic.GetAccountById(id);
                if(account != null) {
                    response = request.CreateResponse(HttpStatusCode.OK, account);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpGet, Route("email/{email}")]
        [ResponseType(typeof(Account))]
        public HttpResponseMessage GetAccountByEmail(HttpRequestMessage request, string email) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                Account account = this.Logic.GetAccountByEmail(email);
                if (account != null) {
                    response = request.CreateResponse(HttpStatusCode.OK, account);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, Account val) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                if (this.Logic.EditAccount(val)) {
                    response = request.CreateResponse(HttpStatusCode.NoContent);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpDelete, Route("{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try {
                if (this.Logic.DeleteAccount(id)) {
                    response = request.CreateResponse(HttpStatusCode.Accepted);
                }
            } catch (Exception) {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
