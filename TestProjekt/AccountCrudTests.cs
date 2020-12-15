using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServices.Models;
using RESTServices.Database;
using System.Data.SqlClient;
using System.Configuration;

namespace TestProjekt {
    [TestClass]
    public class AccountCrudTests {

        /*
         * For this test to work, it is a requirement for all test methods to run within the same instance, and that they run in order
         * This can be done by selecting the group as a whole within the testing window and testing them there
         */
        /*
        public static int accountId;
        public static Account generatedAccount;
        public static Account DatabaseAccount;
        public static Account UpdatedAccount;
        public AccountDB db;

        [TestInitialize]
        public void StartSetup() {
            generatedAccount = new Account() {
                Email = "test@test.tet",
                Name = "SnoopG",
                Phone = "88888888"
            };
            db = new AccountDB();
        }

        [TestMethod]
        public void Test10_Create() {
            //Arrange
            bool result = false;

            //Act
            var returnedObject = db.Create(generatedAccount);
            if(returnedObject is int && returnedObject != null) {
                accountId = (int)returnedObject;
                generatedAccount.Id = accountId;
                result = true;
            }

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Test20_Get() {
            //Arrange
            bool result = false;
            //Act
            var account = db.Get(accountId);
            if(account is Account && account != null) {
                DatabaseAccount = (Account)account;
                result = true;
            }

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Test30_Update() {
            //Arrange
            Account changedGeneratedAccount = new Account() {
                Email = "test@test.tet",
                Name = "Echo",
                Phone = "88888888",
                Id = accountId
            };

            //Act
            db.Update(changedGeneratedAccount);
            var account = db.Get(accountId);
            if (account is Account && account != null) {
                UpdatedAccount = (Account)account;
            }

            //Assert
            Assert.AreNotEqual(DatabaseAccount.Name, UpdatedAccount.Name);
        }

        [TestMethod]
        public void Test40_Delete() {
            //Arrange
            bool result = false;

            //Act
            var id = db.Delete(accountId);
            if(id is int && id != null && (int)id == accountId) {
                result = true;
            }

            //Assert
            Assert.IsTrue(result);

        }
        */
    }
}
