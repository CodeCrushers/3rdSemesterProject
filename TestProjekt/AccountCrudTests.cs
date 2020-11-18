using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServices.Models;
using RESTServices.Database;
using System.Data.SqlClient;
using System.Configuration;

namespace TestProjekt {
    [TestClass]
    public class AccountCrudTests {
        private int accountId;
        private Account account;
        private AccountDB accountDB;


        [TestInitialize]
        public void StartSetup() {
            account = new Account() {
                Name = "Ole",
                Email = "OKOKOK@Gmail.com",
                Phone = "31645121"
            };
            accountDB = new AccountDB();
            accountId = (int) accountDB.Create(account);

        }

        [TestMethod]
        public void Test10_Create() {

            Account accountFromDB;
            accountFromDB = accountDB.Get(accountId);
            Console.WriteLine("CREATE METHOD" + "Account Id = " + accountId);
            account.Id = accountId;
            Console.WriteLine(account.Id + account.Name + account.Email + account.Phone);
            Console.WriteLine(accountFromDB.Id + accountFromDB.Name + accountFromDB.Email + accountFromDB.Phone);

            Assert.AreEqual(account, accountFromDB);
            //Assert.IsTrue(account.Equals(accountFromDB));
        }

        [TestMethod]
        public void Test20_Get() {

            Account accountFromDB;
            Console.WriteLine("GET METHOD " + accountId);
            accountFromDB = accountDB.Get(accountId);

            Assert.AreEqual(account, accountFromDB);
            //Assert.IsTrue(account.Equals(accountFromDB));


        }

        [TestMethod]

        public void Test30_Update() {
            Account updateAccount = new Account() {
                Id = accountId,
                Name = "Ikke Ole",
                Email = "NyEmail@gmail.com",
                Phone = "15469712",
            };
            accountDB.Update(updateAccount);
            Account accountFromDB = accountDB.Get(accountId);
            Assert.AreNotEqual(account, accountFromDB);

        }

        [TestMethod]
        public void Test40_Delete() {
            Account accountFromDB = null;
            accountDB.Delete(accountId);
            try {
            accountFromDB = accountDB.Get(accountId);

            }
            catch (InvalidOperationException e) {

            }

            Assert.IsNull(accountFromDB);

        }

    }
}
