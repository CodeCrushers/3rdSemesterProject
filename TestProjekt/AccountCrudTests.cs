using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServices.Models;
using RESTServices.Database;
using System.Data.SqlClient;
using System.Configuration;

namespace TestProjekt {
    [TestClass]
    public class AccountCrudTests {

        private Account account = new Account() {
            Name = "Ole",
            Email = "OKOKOK@Gmail.com",
            Phone = "31645121"
        };
        private AccountDB accountDB = new AccountDB();


        [TestInitialize]
        public void StartSetup() {

        }

        [TestMethod]
        public void CreateTest() {

            accountDB.Create(account);
            using(SqlConnection con = new SqlConnection(ConfigurationManager)
            accountDB.Get();


            Assert.AreEqual(account)
        }
        [TestMethod]
        public void GetTest() {

        }
    }
}
