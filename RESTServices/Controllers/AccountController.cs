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

        [HttpGet]
        public IEnumerable<Account> Get() {
            return db.GetAll();
        }

        [HttpGet]
        public Account Get(string email) {
            return db.Get(email);
        }

        [HttpPost]
        public void Post(Account val) {
            db.Create(val);
        }

        [HttpPut]
        public void Put(Account val) {
            db.Update(val);
        }

        [HttpDelete, Route("{id}")]
        public void Delete(int id) {
            db.Delete(id);
        }
    }
}
