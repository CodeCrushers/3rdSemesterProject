﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Transactions;
using RESTServices.Models;
using System.Configuration;

namespace RESTServices.Database {
    public class BookingDB : ICRUD<Booking> {

        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public object Create(Booking entity) {
            object id = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand carCmd = con.CreateCommand()) {
                        carCmd.CommandText = "SELECT * FROM Cars WHERE registrationNumber = @registrationNumber";
                        carCmd.Parameters.AddWithValue("registrationNumber", entity.BookingCar.RegistrationNumber);
                        var carReader = carCmd.ExecuteReader();
                        Car car = CarDB.CreateObject(carReader, true);
                        carReader.Close();
                        if (car.Occupied == false) {
                            car.Occupied = true;
                            using (SqlCommand carUpdateCommand = con.CreateCommand()) {
                                carUpdateCommand.CommandText = "UPDATE Cars SET onRoute = @onRoute WHERE registrationNumber = @registrationNumber";
                                carUpdateCommand.Parameters.AddWithValue("onRoute", car.Occupied);
                                carUpdateCommand.Parameters.AddWithValue("registrationNumber", car.RegistrationNumber);
                                carUpdateCommand.ExecuteNonQuery(); 
                            }
                            using (SqlCommand cmd = con.CreateCommand()) {
                                cmd.CommandText = "INSERT INTO Booking (payedFor, paymentAmount, startLocationId, endLocation, bookingDate, bookingRegistrationNumber, accountId)"
                                                  + "OUTPUT INSERTED.id VALUES(@payedFor, @paymentAmount, @startLocationId, @endLocation, @bookingDate, @bookingRegistrationNumber, @accountId)";
                                cmd.Parameters.AddWithValue("payedFor", entity.PayedFor);
                                cmd.Parameters.AddWithValue("paymentAmount", entity.PaymentAmount);
                                cmd.Parameters.AddWithValue("startLocationId", entity.StartLocation);
                                cmd.Parameters.AddWithValue("endLocation", entity.EndLocation);
                                cmd.Parameters.AddWithValue("bookingDate", entity.BookingDate);
                                cmd.Parameters.AddWithValue("bookingRegistrationNumber", entity.BookingCar.RegistrationNumber);
                                cmd.Parameters.AddWithValue("accountId", entity.Account.Id);
                                id = cmd.ExecuteScalar();
                                con.Close();
                            }  
                            scope.Complete();
                        } else {
                            scope.Dispose();
                        }
                    }
                }
            }
            return id;

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
            Booking booking = new Booking() {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                PayedFor = reader.GetBoolean(reader.GetOrdinal("payedFor")),
                PaymentAmount = reader.GetDouble(reader.GetOrdinal("paymentAmount")),
                StartLocation = reader.GetString(reader.GetOrdinal("startLocationId")),
                EndLocation = reader.GetString(reader.GetOrdinal("endLocation")),
                BookingDate = reader.GetDateTime(reader.GetOrdinal("bookingDate")),
            };
            return booking;
        }

        public Booking Get(object var) {
            Booking booking = null;
            if (var is int) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            cmd.CommandText = "SELECT * FROM Booking WHERE id = @id";
                            cmd.Parameters.AddWithValue("id", var);
                            var reader = cmd.ExecuteReader();
                            booking = CreateObject(reader, true);
                            string registrationNumber = reader.GetString(reader.GetOrdinal("bookingRegistrationNumber"));
                            int accountId = reader.GetInt32(reader.GetOrdinal("accountId"));
                            reader.Close();
                            using (SqlCommand carCmd = con.CreateCommand()) {
                                carCmd.CommandText = "SELECT * FROM Cars WHERE registrationNumber = @registrationNumber";
                                carCmd.Parameters.AddWithValue("registrationNumber", registrationNumber);
                                var carReader = carCmd.ExecuteReader();
                                Car car = CarDB.CreateObject(carReader, true);
                                booking.BookingCar = car;
                                carReader.Close();
                            }
                            using (SqlCommand accountCmd = con.CreateCommand()) {
                                accountCmd.CommandText = "SELECT * FORM Accounts WHERE id = @id";
                                accountCmd.Parameters.AddWithValue("id", accountId);
                                var accountReader = accountCmd.ExecuteReader();
                                Account account = AccountDB.CreateObject(accountReader, true);
                                booking.Account = account;
                                accountReader.Close();
                            }
                        }
                    }
                    scope.Complete();
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
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "UPDATE Booking SET payedFor = @payedFor, paymentAmount = @paymentAmount, startLocationId = @startLocation, endLocation = @endLocation," +
                            " bookingDate = @bookingDate, bookingRegistrationNumber = @bookingRegistrationNumber, accountId = @accountId WHERE id = @id";
                        cmd.Parameters.AddWithValue("payedFor", entity.PayedFor);
                        cmd.Parameters.AddWithValue("paymentAmount", entity.PaymentAmount);
                        cmd.Parameters.AddWithValue("startLocation", entity.StartLocation);
                        cmd.Parameters.AddWithValue("endLocation", entity.EndLocation);
                        cmd.Parameters.AddWithValue("bookingDate", entity.BookingDate);
                        cmd.Parameters.AddWithValue("bookingRegistrationNumber", entity.BookingCar.RegistrationNumber);
                        cmd.Parameters.AddWithValue("accoutnId", entity.Account.Id);
                        cmd.Parameters.AddWithValue("id", entity.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                scope.Complete();
            }
        }

        public object Delete(object var) {
            object o = null;
            if (var is int) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            cmd.CommandText = "DELETE FROM Booking OUTPUT DELETED.id WHERE id = @id";
                            cmd.Parameters.AddWithValue("id", var);
                            o = cmd.ExecuteScalar();
                        }
                    }
                    scope.Complete();
                } 
            }
            return o;
        }
    }
}