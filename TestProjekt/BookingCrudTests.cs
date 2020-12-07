using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTServices.Database;
using RESTServices.Models;

namespace TestProjekt {
    [TestClass]
    public class BookingCrudTests {

        /*
         * For this test to work, it is a requirement for all test methods to run within the same instance, and that they run in order
         * This can be done by selecting the group as a whole within the testing window and testing them there
         */

        public static Booking entity;
        public static int bookingId;
        public static Booking DatabaseBooking;
        public static Booking UpdatedDatabaseBooking;
        public static Car car;
        public static DateTime time;

        [TestInitialize]
        public void Init() {
            CarDB carDB = new CarDB();
            time = DateTime.Now;
            car = carDB.Get("123XD12"); //Also needs to be an excisting Car Registration number for test to work
            entity = new Booking() {
                PayedFor = true,
                PaymentAmount = 20.0,
                StartLocation = "Gachi the right way",
                EndLocation = "SnnopG's Entry way",
                BookingCar = car,
                BookingDate = time
            };
        }

        [TestMethod]
        public void TestA_CreateBookingObject() {
            //Arrange
            bool result = false;
            BookingDB db = new BookingDB();

            //Act
            var id = db.Create(entity);
            if (id is int) {
                result = true;
                bookingId = (int)id;
                entity.Id = bookingId;
            }

            //Assert
            Assert.IsTrue(result); 
        }

        [TestMethod]
        public void TestB_GetBookingObject() {
            //Arrange
            bool result = false;
            BookingDB db = new BookingDB();

            //Act
            var b = db.Get(bookingId); //Number inside method needs to be an excisting ID for a booking for test to return positive, else negative results are returned.
            if (b is Booking) {
                result = true;
                DatabaseBooking = (Booking)b;
            }

            //Assert
            Assert.IsTrue(result); 
        }

        [TestMethod]
        public void TestC_UpdateBookingObject() {
            //Arrange
            Booking changedBooking = new Booking() {
                Id = bookingId,
                PayedFor = true,
                PaymentAmount = 25.0,
                StartLocation = "Gachi the right way",
                EndLocation = "SnnopG's Entry way",
                BookingCar = car,
                BookingDate = time
            };
            BookingDB db = new BookingDB();

            //Act
            db.Update(changedBooking);
            var b = db.Get(bookingId); //Number inside method needs to be an excisting ID for a booking for test to return positive, else negative results are returned.
            if (b is Booking) {
                UpdatedDatabaseBooking = (Booking)b;
            }

            //Assert
            Assert.AreNotEqual(DatabaseBooking.PaymentAmount, UpdatedDatabaseBooking.PaymentAmount);

        }

        [TestMethod]
        public void TestD_DeleteObject() {
            //Arrange
            BookingDB db = new BookingDB();
            bool result = false;

            //Act
            var id = db.Delete(bookingId); //Number inside method needs to be an excisting ID for a booking for test to return positive, else negative results are returned.
            if (id is int) {
                if((int)id == bookingId) {
                    result = true;
                }
            }

            //Assert
            Assert.IsTrue(result);
        }
    }
}
