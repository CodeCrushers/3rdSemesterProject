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
    public class AccountController : ApiController
    {

        private AccountDB accountDB = new AccountDB();
        // GET: api/Account
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return accountDB.GetAll();
        }

        // GET: api/Account/testemail@gmail.com / 1
        [HttpGet]
        public Account Get(string email)
        {
            Account account = accountDB.Get(email);
            if (account is null) {
                account = new Account {
                    Name = "Fail",
                    Email = "Fail",
                    Id = -1,
                    Phone = "0000000"

                };
            }
            return account;
        }



        // POST: api/Account
        public IHttpActionResult Post([FromBody]Account account)
        {
            var id = accountDB.Create(account);
            if (id == null) {
                return InternalServerError();
            }
            return Created<Account>("Accounts", account);
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
