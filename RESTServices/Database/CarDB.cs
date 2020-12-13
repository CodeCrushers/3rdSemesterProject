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

        public object Create(Car entity) {
            object  variable = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "INSERT INTO Cars (brand, model, registrationNumber) OUTPUT INSERTED.registrationNumber VALUES (@brand, @model, @registrationNumber) ";
                        cmd.Parameters.AddWithValue("brand", entity.Brand);
                        cmd.Parameters.AddWithValue("model", entity.Model);
                        cmd.Parameters.AddWithValue("registrationNumber", entity.RegistrationNumber);
                        variable = cmd.ExecuteScalar();
                    }
                }
                scope.Complete();
            }
            return variable;
        }


        public object Delete(object reg) {
            object var = null;
            if (reg is string) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            cmd.CommandText = "DELETE FROM Cars OUTPUT DELETED.id WHERE id = @id";
                            cmd.Parameters.AddWithValue("id", reg);
                            var = cmd.ExecuteScalar();
                        }
                    }
                }
            }
            return var;
        }

        public Car Get(object var) {
            Car car = null;
            if (var is string) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            cmd.CommandText = "SELECT * FROM Cars WHERE registrationNumber = @registrationNumber";
                            cmd.Parameters.AddWithValue("registrationNumber", var);
                            var reader = cmd.ExecuteReader();
                            car = CreateObject(reader, true);
                        }
                    }
                    scope.Complete();
                } 
            }
            return car;
        }

        public IEnumerable<Car> GetAll() {
            IEnumerable<Car> list = null;
            using(TransactionScope scope = new TransactionScope()) {
                using(SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using(SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "SELECT * FROM Cars";
                        var reader = cmd.ExecuteReader();
                        list = CreateList(reader);
                    }
                }
                scope.Complete();
            }
            return list;
        }

        public bool Update(Car entity) {
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "UPDATE Cars SET brand = @brand, model = @model, registrationNumber = @registrationNumber";
                        cmd.Parameters.AddWithValue("brand", entity.Brand);
                        cmd.Parameters.AddWithValue("model", entity.Model);
                        cmd.Parameters.AddWithValue("registrationNumber", entity.RegistrationNumber);
                        cmd.ExecuteNonQuery();
                    }
                }
                scope.Complete();
            }
            throw new NotImplementedException();
        }

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
            car.Occupied = reader.GetBoolean(reader.GetOrdinal("onRoute"));
            return car;
        }

    }
}