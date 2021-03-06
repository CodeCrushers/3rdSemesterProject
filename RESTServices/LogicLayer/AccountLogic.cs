﻿using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTServices.LogicLayer {
    public class AccountLogic {

        private readonly AccountDB _accountDB;

        public AccountLogic() {
            this._accountDB = new AccountDB();
        }

        public bool CreateAccount(Account entity) {
            bool result = false;
            object o = this._accountDB.Create(entity);
            if(o is string) {
                result = true;
            } else if(o is bool) {
                if((bool)o == false) {
                    result = false;
                }
            }
            return result;
        }

        public Account GetAccountById(string id) {
            Account account = null;
            account = this._accountDB.GetAccountById(id);
            return account;
        }

        public Account GetAccountByEmail(string email) {
            Account account = null;
            account = this._accountDB.GetAccountByEmail(email);
            return account;
        }

        public IEnumerable<Account> GetAllAccounts() {
            IEnumerable<Account> list = null;
            list = this._accountDB.GetAll();
            return list;
        }

        public bool EditAccount(Account entity) {
            bool result = false;
            result = this._accountDB.Update(entity);
            return result;
        }

        public bool DeleteAccount(string id) {
            bool result = false;
            object o = this._accountDB.Delete(id);
            if (o is string) {
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