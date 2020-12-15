using RESTServices.Database;
using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace RESTServices.LogicLayer {
    public class BookingLogic {

        private readonly BookingDB _bookingDB;
        private readonly AccountDB _accountDB;
        private readonly CarDB _carDB;

        public BookingLogic() {
            this._bookingDB = new BookingDB();
            this._accountDB = new AccountDB();
            this._carDB = new CarDB();
        }

        public bool CreateBooking(Booking entity) {
            bool result = false;
            using (TransactionScope scope = new TransactionScope()) {
                Car car = _carDB.Get(entity.BookingCar.RegistrationNumber);
                if(car.OnRoute == false) {
                    car.OnRoute = true;
                    _carDB.Update(car);
                    object o = this._bookingDB.Create(entity);
                    result = true;
                }
                scope.Complete();
            }
            return result;
        }

        public Booking GetBookingById(string id) {
            Booking booking = null;
            booking = _bookingDB.GetBookingById(id);
            Car car = _carDB.Get(booking.BookingCar.RegistrationNumber);
            Account account = _accountDB.Get(booking.Account.Id);
            if (car != null) {
                booking.BookingCar = car;
            }
            if (account != null) {
                booking.Account = account;
            }
            return booking;
        }

        public IEnumerable<Booking> GetBookingsByAccountId(string accountId) {
            IEnumerable<Booking> list = null;
            list = this._bookingDB.GetBookingAccountId(accountId);
            return list;
        }

        public IEnumerable<Booking> GetAllBookings() {
            IEnumerable<Booking> list = null;
            list = this._bookingDB.GetAll();
            return list;
        }

        public bool EditBooking(Booking entity) {
            bool result = false;
            result = this._bookingDB.Update(entity);
            return result;
        }

        public bool DeleteBooking(string id) {
            bool result = false;
            object o = this._bookingDB.Delete(id);
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