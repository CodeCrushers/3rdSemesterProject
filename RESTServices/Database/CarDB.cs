using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RESTServices.Database {

    public class CarDB : ICRUD<Car> {
        private string _connectionString = ConfigurationManager.ConnectionStrings["HildurConnection"].ConnectionString;
        public void Create(Car entity) {
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "INSERT INTO Cars (brand, model, registrationNumber) VALUES (@brand, @model, @registrationNumber)";
                    cmd.Parameters.AddWithValue("brand", entity.Manufacturer);
                    cmd.Parameters.AddWithValue("model", entity.Name);
                    cmd.Parameters.AddWithValue("registrationNumber", entity.RegistrationNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Car> CreateList(SqlDataReader reader) {
            throw new NotImplementedException();
        }

        public Car CreateObject(SqlDataReader reader, bool singleRead) {
            if (singleRead) {
                reader.Read();
            }
            Car car = new Car() {
                Brand = reader.GetString(reader.GetOrdinal("brand")),
                Model = reader.GetString(reader.GetOrdinal("model")),
                RegistrationNumber = reader.GetString(reader.GetOrdinal("registrationNumber")),
                LeasingYear = reader.GetString(reader.GetOrdinal("leasingYear")),
                Distance = reader.GetInt32(reader.GetOrdinal("distance")),
                Charge = reader.GetInt32(reader.GetOrdinal("charge")),
                Capacity = reader.GetInt32(reader.GetOrdinal("capacity"))
                
            };
            return car;
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }


        public Car Get(string registrationNumber) {
            Car car = null;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Cars WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", registrationNumber);
                    var reader = cmd.ExecuteReader();
                    car = CreateObject(reader, true);
                }
            }
            return car;
        }

        public IEnumerable<Car> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Car entity) {
            throw new NotImplementedException();
        }
    }
}