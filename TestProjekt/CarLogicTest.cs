using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                this.Car = null;
            } catch (Exception) {
                result = false;
            }

            //Assert
            Assert.IsTrue(result);
        }

        [TestCleanup]
        public void CleanUp() {
            if(this.Car != null) {
                this.Logic.DeleteCar(this.Car.RegistrationNumber);
            }
        }

    }
}
