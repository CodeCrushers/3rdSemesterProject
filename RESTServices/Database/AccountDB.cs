using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RESTServices.Database {
    public class AccountDB : ICRUD<Account> {

        private string _connectionString = ConfigurationManager.ConnectionStrings["HildurConnection"].ConnectionString;

        public object Create(Account entity) {
            int id;
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "INSERT INTO Accounts (name, email, phonenumber) OUTPUT INSERTED.id VALUES (@name, @email, @phonenumber)";
                    cmd.Parameters.AddWithValue("name", entity.Name);
                    cmd.Parameters.AddWithValue("email", entity.Email);
                    cmd.Parameters.AddWithValue("phonenumber", entity.Phone);
                    id = (int) cmd.ExecuteScalar();
                }
            }
            return id;
        }

        public void Delete(int id) {
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "DELETE FROM Accounts WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Account Get(int id) {
            Account account = null;
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Accounts WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    var reader = cmd.ExecuteReader();
                    account = CreateObject(reader, true);
                }
            }
            return account;
        }

        public IEnumerable<Account> GetAll() {
            IEnumerable<Account> accounts;
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "SELECT id, name, email, phonenumber FROM Accounts";
                    var reader = cmd.ExecuteReader();
                    accounts = CreateList(reader);
                }
            }
            return accounts;
        }

        public void Update(Account entity) {
            using(SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "UPDATE Accounts SET name = @name, email = @email, phonenumber = @phonenumber WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", entity.Id);
                    cmd.Parameters.AddWithValue("name", entity.Name);
                    cmd.Parameters.AddWithValue("email", entity.Email);
                    cmd.Parameters.AddWithValue("phonenumber", entity.Phone);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Account> CreateList(SqlDataReader reader) {
            List<Account> accounts = new List<Account>();
            while(reader.Read()) {
                Account a = CreateObject(reader, false);
                accounts.Add(a);
            }
            return accounts;
        }

        public Account CreateObject(SqlDataReader reader, bool singleRead) {
            if(singleRead) {
                reader.Read();
            }
            Account a = new Account() {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                Phone = reader.GetString(reader.GetOrdinal("phonenumber"))
            };
            return a;
        }

    }
}