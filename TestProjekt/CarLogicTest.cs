using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServices.Database;
using RESTServices.LogicLayer;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjekt {

    [TestClass]
    public class CarLogicTest {

        public CarLogic Logic;
        public Car Car;

        [TestInitialize]
        public void Init() {
            this.Logic = new CarLogic();
            this.Car = new Car() {
                Brand = "Toyota",
                Model = "Avensis",
                RegistrationNumber = "789X789",
                LeasingYear = "1997",
                Distance = 1000,
                Charge = 199,
                Capacity = 5,
                LocationId = "Egevænget 42",
                OnRoute = false,
            };
        }

        [TestMethod]
        public void CreateCarTest() {
            //Arrange
            bool result;
            //Act
            try {
                result = this.Logic.CreateCar(this.Car);
            } catch (Exception) {
                this.Car = null;
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetCarTest() {
            //Arrange
            //Act
            //Assert
        }

        [TestCleanup]
        public void CleanUp() {
            if(this.Car != null) {
                this.Logic.DeleteCar(this.Car.RegistrationNumber);
            }
        }

    }
}
