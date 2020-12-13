using RESTServices.ControlLayer;
using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTServices.Controllers {

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController {

        private AccountDB db = new AccountDB();
        private FormValidation validation = new FormValidation();

        [HttpGet]
        public IEnumerable<Account> Get() {
            return db.GetAll();
        }

        [HttpGet, Route("{email}")]
        public Account Get(string email) {
            return db.Get(email);
        }

        [HttpPost]
        public void Post(Account val) {
            if(val != null) {
                db.Create(val);
            }
            /*
            HttpStatusCode code;
            if (validation.PasswordValidation(val.Password) && validation.EmailValidation(val.Email)) {
                code = HttpStatusCode.Accepted;
            } else {
                code = HttpStatusCode.BadRequest;
            }

            return code;
            */
        }

        [HttpPut]
        public HttpStatusCode Put(Account val) {
            HttpStatusCode code;
            if(validation.PasswordValidation(val.Password) && validation.EmailValidation(val.Email)) {
                db.Update(val);
                code = HttpStatusCode.Created;
            } else {
                code = HttpStatusCode.BadRequest;
            }
            return code;
        }

        [HttpDelete, Route("{id}")]
        public void Delete(int id) {
             db.Delete(id); 
        }
    }
}
