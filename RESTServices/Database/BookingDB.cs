using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Transactions;
using RESTServices.Models;
using System.Configuration;

namespace RESTServices.Database {
    public class BookingDB {

        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public object Create(Booking entity) {
            object o = null;
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    try {
                        cmd.CommandText = "INSERT INTO Booking (payedFor, paymentAmount, startLocation, endLocation, bookingDate, carRegistrationNumber, accountId) OUTPUT INSERTED.id " +
                            "VALUES (@payedFor, @paymentAmount, @startLocation, @endLocation, @bookingDate, @carRegistrationNumber, @accountId)";
                        cmd.Parameters.AddWithValue("payedFor", entity.PayedFor);
                        cmd.Parameters.AddWithValue("paymentAmount", entity.PaymentAmount);
                        cmd.Parameters.AddWithValue("startLocation", entity.StartLocation);
                        cmd.Parameters.AddWithValue("endLocation", entity.EndLocation);
                        cmd.Parameters.AddWithValue("bookingDate", entity.BookingDate);
                        cmd.Parameters.AddWithValue("carRegistrationNumber", entity.BookingCar.RegistrationNumber);
                        cmd.Parameters.AddWithValue("accountId", entity.Account.Id);
                        o = cmd.ExecuteScalar();
                    } catch(Exception e) {
                        o = false;
                        throw e;
                    }
                }
            }
            return o;
        }

        public Booking GetBookingById(string id) {
            Booking booking = null;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    try {
                        cmd.CommandText = "SELECT * FROM Booking WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                        var reader = cmd.ExecuteReader();
                        booking = CreateObject(reader, true);
                    } catch (Exception e) {
                        throw e;
                    }
                }
            }
            return booking;
        }

        public IEnumerable<Booking> GetBookingAccountId(string id) {
            IEnumerable<Booking> bookings = null;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    try {
                        cmd.CommandText = "SELECT * FROM Booking WHERE accountId = @id";
                        cmd.Parameters.AddWithValue("id", id);
                        var reader = cmd.ExecuteReader();
                        bookings = CreateList(reader);
                    } catch (Exception e) {
                        throw e;
                    }
                }
            }
            return bookings;
        }

        public IEnumerable<Booking> GetAll() {
            IEnumerable<Booking> bookings = null;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    try {
                        cmd.CommandText = "SELECT * FROM Booking";
                        var reader = cmd.ExecuteReader();
                        bookings = CreateList(reader);
                    } catch (Exception e) {
                        throw e;
                    }
                }
            }
            return bookings;
        }

        public bool Update(Booking entity) {
            bool result = true;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    try {
                        cmd.CommandText = "UPDATE Booking SET payedFor = @payedFor, paymentAmount = @paymentAmount, startLocation = @startLocation, endLocation = @endLocation," +
                                            " bookingDate = @bookingDate, bookingRegistrationNumber = @bookingRegistrationNumber, accountId = @accountId WHERE id = @id";
                        cmd.Parameters.AddWithValue("payedFor", entity.PayedFor);
                        cmd.Parameters.AddWithValue("paymentAmount", entity.PaymentAmount);
                        cmd.Parameters.AddWithValue("startLocation", entity.StartLocation);
                        cmd.Parameters.AddWithValue("endLocation", entity.EndLocation);
                        cmd.Parameters.AddWithValue("bookingDate", entity.BookingDate);
                        cmd.Parameters.AddWithValue("bookingRegistrationNumber", entity.BookingCar.RegistrationNumber);
                        cmd.Parameters.AddWithValue("accountId", entity.Account.Id);
                        cmd.Parameters.AddWithValue("id", entity.Id);
                        cmd.ExecuteNonQuery();
                    } catch (Exception e) {
                        result = false;
                        throw e;
                    }
                }
            }
            return result;
        }

        public object Delete(object var) {
            object o = null;
            if (var is int) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "DELETE FROM Booking OUTPUT DELETED.id WHERE id = @id";
                            cmd.Parameters.AddWithValue("id", var);
                            o = cmd.ExecuteScalar();
                        } catch (Exception e) {
                            o = false;
                            throw e;
                        }
                    }
                } 
            } else {
                o = false;
            }
            return o;
        }

        /*
         * These methods below are here to create objects of type of this DB class
         */

        public IEnumerable<Booking> CreateList(SqlDataReader reader) {
            List<Booking> bookings = new List<Booking>();
            while (reader.Read()) {
                Booking booking = CreateObject(reader, false);
                bookings.Add(booking);
            }
            return bookings;
        }

        public static Booking CreateObject(SqlDataReader reader, bool singleRead) {
            if (singleRead) {
                reader.Read();
            }
            Booking booking = new Booking() {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                PayedFor = reader.GetBoolean(reader.GetOrdinal("payedFor")),
                PaymentAmount = reader.GetDouble(reader.GetOrdinal("paymentAmount")),
                StartLocation = reader.GetString(reader.GetOrdinal("startLocation")),
                EndLocation = reader.GetString(reader.GetOrdinal("endLocation")),
                BookingDate = reader.GetDateTime(reader.GetOrdinal("bookingDate")),
                Account = new Account() { Id = reader.GetString(reader.GetOrdinal("accountId"))},
                BookingCar = new Car() { RegistrationNumber = reader.GetString(reader.GetOrdinal("carRegistrationNumber")) }
            };
            return booking;
        }
    }
}