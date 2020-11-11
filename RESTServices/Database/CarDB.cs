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
                    cmd.Parameters.AddWithValue("registrationNumber", entity.SerialNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public Car Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Car entity) {
            throw new NotImplementedException();
        }
    }
}