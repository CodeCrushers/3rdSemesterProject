using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.LogicLayer {
    public class AccountLogic {

        private AccountDB accountDB;

        public AccountLogic() {
            this.accountDB = new AccountDB();
        }

        public bool CreateAccount(Account entity) {
            bool result = false;
            object o = this.accountDB.Create(entity);
            if(o is int) {
                result = true;
            } else if(o is bool) {
                if((bool)o == false) {
                    result = false;
                }
            }
            return result;
        }
        public Account GetAccount(string mail) {
            throw new NotImplementedException();
        }
        public IEnumerable<Account> GetAllAccounts() {
            throw new NotImplementedException();
        }

        public bool EditAccount(Account entity) {
            throw new NotImplementedException();
        }


        public bool DeleteAccount(int id) {
            throw new NotImplementedException();
        }


    }
}