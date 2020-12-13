using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.LogicLayer {
    public class AccountLogic {

        private AccountDB AccountDB;

        public AccountLogic() {
            this.AccountDB = new AccountDB();
        }

        public bool CreateAccount(Account entity) {
            bool result = false;
            object o = this.AccountDB.Create(entity);
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
            Account account = null;
            account = this.AccountDB.Get(mail);
            return account;
        }
        public IEnumerable<Account> GetAllAccounts() {
            IEnumerable<Account> list = null;
            list = this.AccountDB.GetAll();
            return list;
        }

        public bool EditAccount(Account entity) {
            bool result = false;
            result = this.AccountDB.Update(entity);
            return result;
        }

        public bool DeleteAccount(int id) {
            bool result = false;
            object o = this.AccountDB.Delete(id);
            if (o is int) {
                result = true;
            } else if (o is bool) {
                if ((bool)o == false) {
                    result = false;
                }
            }
            return result;
        }


    }
}