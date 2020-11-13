using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RESTServices.Models;
using System.Configuration;

namespace RESTServices.Database {
    public class BookingDB : ICRUD<Booking> {

        private string _connectionString = ConfigurationManager.ConnectionStrings["HildurConnection"].ConnectionString;

        public void Create(Booking entity) {
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "INSERT INTO Booking (payedFor, paymentAmount, startLocationId, endLocationId, bookingDate, bookingRegistrationNumber)"
                                      + "VALUES(@payedFor, @paymentAmount, @startLocationId, @endLocationId, @bookingDate, @bookingRegistrationNumber)";

                    cmd.Parameters.AddWithValue("payedFor", ConvertToBinary(entity.PayedFor));
                    cmd.Parameters.AddWithValue("paymentAmount", entity.PaymentAmount);
                    cmd.Parameters.AddWithValue("startLocationId", entity.StartLocation);
                    cmd.Parameters.AddWithValue("endLocationId", entity.EndLocation);
                    cmd.Parameters.AddWithValue("bookingDate", entity.BookingDate);
                    cmd.Parameters.AddWithValue("bookingRegistrationNumber", entity.BookingCar.RegistrationNumber);
                    cmd.ExecuteNonQuery();

                }


            }

        }

        public IEnumerable<Booking> CreateList(SqlDataReader reader) {
            List<Booking> bookings = new List<Booking>();
            while(reader.Read()) {
                Booking booking = CreateObject(reader, false);
                bookings.Add(booking);
            }
            return bookings;
        }

        public Booking CreateObject(SqlDataReader reader, bool singleRead) {
            if (singleRead) {
                reader.Read();
            }
            CarDB carDB = new CarDB();
            Booking booking = new Booking() {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                PayedFor = ConvertToBoolean(reader.GetInt32(reader.GetOrdinal("payedFor"))),
                PaymentAmount = (double)reader.GetFloat(reader.GetOrdinal("paymentAmount")),
                StartLocation = reader.GetString(reader.GetOrdinal("startLocation")),
                EndLocation = reader.GetString(reader.GetOrdinal("endLocation")),
                BookingDate = reader.GetDateTime(reader.GetOrdinal("bookingDate")),
                BookingCar = carDB.Get(reader.GetString(reader.GetOrdinal("registrationNumber")))

            };
            return booking;
        }

        public void Delete(int id) {
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "DELETE FROM Booking WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Booking Get(int id) {
            Booking booking = null;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Booking WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    var reader = cmd.ExecuteReader();
                    booking = CreateObject(reader, true);
                }
            }
            return booking;
        }

        public IEnumerable<Booking> GetAll() {
            IEnumerable<Booking> bookings;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Booking";
                    var reader = cmd.ExecuteReader();
                    bookings = CreateList(reader);
                }
            }
            return bookings;
        }

        public void Update(Booking entity) {
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "UPDATE Booking SET payedFor = @payedFor, paymentAmount = @paymentAmount, startLocation = @startLocation, endLocation = @endLocation," +
                        " bookingDate = @bookingDate, bookingRegistrationNumber = @bookingRegistrationNumber WHERE id = @id";
                    cmd.Parameters.AddWithValue("payedFor", entity.PayedFor);
                    cmd.Parameters.AddWithValue("paymentAmount", entity.PaymentAmount);
                    cmd.Parameters.AddWithValue("startLocation", entity.StartLocation);
                    cmd.Parameters.AddWithValue("endLocation", entity.EndLocation);
                    cmd.Parameters.AddWithValue("bookingDate", entity.BookingDate);
                    cmd.Parameters.AddWithValue("bookingRegistration", entity.BookingCar.RegistrationNumber);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ConvertToBinary(bool boolean) {
            int result = 0;
            if (boolean) {
                result = 1;
            }
            return result;
        }

        public bool ConvertToBoolean(int binary) {
            bool result = false;
            if (binary == 1) {
                result = true;
            }
            return result;
        }
    }
}