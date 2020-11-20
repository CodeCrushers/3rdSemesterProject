using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace RESTServices.Database {
    public class AccountDB : ICRUD<Account> {

        private static string _connectionString = ConfigurationManager.ConnectionStrings["HildurConnection"].ConnectionString;

        public AccountDB() {
        }

        public object Create(Account entity) {
            int id;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "INSERT INTO Accounts (name, email, phonenumber) OUTPUT INSERTED.id VALUES (@name, @email, @phonenumber)";
                        cmd.Parameters.AddWithValue("name", entity.Name);
                        cmd.Parameters.AddWithValue("email", entity.Email);
                        cmd.Parameters.AddWithValue("phonenumber", entity.Phone);
                        id = (int)cmd.ExecuteScalar();
                    }
                }
                scope.Complete();
            }
            return id;
        }

        public object Delete(int id) {
            Object o = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "DELETE FROM Accounts OUTPUT DELETED.id WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                        o = cmd.ExecuteScalar();
                    }
                }
                scope.Complete();
            }
            return o;
        }

        public Account Get(int id) {
            Account account = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "SELECT * FROM Accounts WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", id);
                        var reader = cmd.ExecuteReader();
                        account = CreateObject(reader, true);
                    }
                }
                scope.Complete();
            }
            return account;
        }

        public static Account Get(string email) {
            Account account = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "SELECT * FROM Accounts WHERE email = @email";
                        cmd.Parameters.AddWithValue("id", email);
                        var reader = cmd.ExecuteReader();
                        if (reader != null) {
                            account = CreateObject(reader, true);
                        }
                    }
                }
                scope.Complete();
            }
            return account;
        }

        public IEnumerable<Account> GetAll() {
            IEnumerable<Account> accounts;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "SELECT id, name, email, phonenumber FROM Accounts";
                        var reader = cmd.ExecuteReader();
                        accounts = CreateList(reader);
                    }
                }
                scope.Complete();
            }
            return accounts;
        }

        public void Update(Account entity) {
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = "UPDATE Accounts SET name = @name, email = @email, phonenumber = @phonenumber WHERE id = @id";
                        cmd.Parameters.AddWithValue("id", entity.Id);
                        cmd.Parameters.AddWithValue("name", entity.Name);
                        cmd.Parameters.AddWithValue("email", entity.Email);
                        cmd.Parameters.AddWithValue("phonenumber", entity.Phone);
                        cmd.ExecuteNonQuery();
                    }
                }
                scope.Complete();
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

        public static Account CreateObject(SqlDataReader reader, bool singleRead) {
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