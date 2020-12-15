using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using System.Web;

namespace RESTServices.Database {

    public class CarDB : ICRUD<Car> {

        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public CarDB() {

        }

        public object Create(Car entity) {
            object o = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "INSERT INTO Car (brand, model, registrationNumber, leasingYear, distance, charge, capacity, locationId, onRoute) OUTPUT INSERTED.registrationNumber " +
                                "VALUES (@brand, @model, @registrationNumber, @leasingYear, @distance, @charge, @capacity, @locationId, @onRoute)";
                            cmd.Parameters.AddWithValue("brand", entity.Brand);
                            cmd.Parameters.AddWithValue("model", entity.Model);
                            cmd.Parameters.AddWithValue("registrationNumber", entity.RegistrationNumber);
                            cmd.Parameters.AddWithValue("leasingYear", entity.LeasingYear);
                            cmd.Parameters.AddWithValue("distance", entity.Distance);
                            cmd.Parameters.AddWithValue("charge", entity.Charge);
                            cmd.Parameters.AddWithValue("capacity", entity.Capacity);
                            cmd.Parameters.AddWithValue("locationId", entity.LocationId);
                            cmd.Parameters.AddWithValue("onRoute", entity.OnRoute);
                            o = cmd.ExecuteScalar();
                        } catch (Exception) {
                            o = false;
                            scope.Dispose();
                        }
                    }
                }
                scope.Complete();
            }
            return o;
        }

        public Car Get(object var) {
            Car car = null;
            if (var is string) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            try {
                                cmd.CommandText = "SELECT * FROM Car WHERE registrationNumber = @registrationNumber";
                                cmd.Parameters.AddWithValue("registrationNumber", var);
                                var reader = cmd.ExecuteReader();
                                car = CreateObject(reader, true);
                            } catch (Exception) {
                                scope.Dispose();
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            return car;
        }

        public IEnumerable<Car> GetAll() {
            IEnumerable<Car> list = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "SELECT * FROM Car";
                            var reader = cmd.ExecuteReader();
                            list = CreateList(reader);
                        } catch (Exception) {
                            scope.Dispose();
                        }
                    }
                }
                scope.Complete();
            }
            return list;
        }

        public bool Update(Car entity) {
            bool result = true;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "UPDATE Car SET " +
                                                "brand = @brand, model = @model, leasingYear = @leasingYear, distance = @distance, charge = @charge, capacity = @capacity, locationId = @locationId, onRoute = @onRoute " +
                                                "WHERE registrationNumber = @registrationNumber";
                            cmd.Parameters.AddWithValue("brand", entity.Brand);
                            cmd.Parameters.AddWithValue("model", entity.Model);
                            cmd.Parameters.AddWithValue("registrationNumber", entity.RegistrationNumber);
                            cmd.Parameters.AddWithValue("leasingYear", entity.LeasingYear);
                            cmd.Parameters.AddWithValue("distance", entity.Distance);
                            cmd.Parameters.AddWithValue("charge", entity.Charge);
                            cmd.Parameters.AddWithValue("capacity", entity.Capacity);
                            cmd.Parameters.AddWithValue("locationId", entity.LocationId);
                            cmd.Parameters.AddWithValue("onRoute", entity.OnRoute);
                            cmd.ExecuteNonQuery();
                        } catch (Exception) {
                            result = false;
                            scope.Dispose();
                        }
                    }
                }
                scope.Complete();
            }
            return result;
        }

        public object Delete(object reg) {
            object o = null;
            if (reg is string) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            try {
                                cmd.CommandText = "DELETE FROM Car OUTPUT DELETED.id WHERE id = @id";
                                cmd.Parameters.AddWithValue("id", reg);
                                o = cmd.ExecuteScalar();
                            } catch (Exception) {
                                o = false;
                                scope.Dispose();
                            }
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
        public IEnumerable<Car> CreateList(SqlDataReader reader) {
            List<Car> bookings = new List<Car>();
            while (reader.Read()) {
                Car car = CreateObject(reader, false);
                bookings.Add(car);
            }
            return bookings;
        }

        public static Car CreateObject(SqlDataReader reader, bool singleRead) {
            if (singleRead) {
                reader.Read();
            }
            Car car = new Car();
            car.Brand = reader.GetString(reader.GetOrdinal("brand"));
            car.Model = reader.GetString(reader.GetOrdinal("model"));
            car.RegistrationNumber = reader.GetString(reader.GetOrdinal("registrationNumber"));
            car.LeasingYear = reader.GetString(reader.GetOrdinal("leasingYear"));
            car.Distance = reader.GetInt32(reader.GetOrdinal("distance"));
            car.Charge = reader.GetInt32(reader.GetOrdinal("charge"));
            car.Capacity = reader.GetInt32(reader.GetOrdinal("capacity"));
            car.OnRoute = reader.GetBoolean(reader.GetOrdinal("onRoute"));
            return car;
        }

    }
}