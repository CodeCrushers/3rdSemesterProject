﻿using RESTServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace RESTServices.Database {
    public class AccountDB : ICRUD<Account> {

        private static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public object Create(Account entity) {
            object o = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "INSERT INTO Account (id, name, email, phonenumber, Password) OUTPUT INSERTED.id VALUES (@id, @name, @email, @phonenumber, @password)";
                            cmd.Parameters.AddWithValue("id", entity.Id);
                            cmd.Parameters.AddWithValue("name", entity.Name);
                            cmd.Parameters.AddWithValue("email", entity.Email);
                            cmd.Parameters.AddWithValue("phonenumber", entity.Phone);
                            cmd.Parameters.AddWithValue("password", entity.Password);
                            o = cmd.ExecuteScalar();
                            scope.Complete();
                        } catch (Exception) {
                            o = false;
                            scope.Dispose();
                        }
                    }
                }
            }
            return o;
        }

        public Account Get(object var) {
            Account account = null;
            if (var is string) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            try {
                                cmd.CommandText = "SELECT * FROM Account WHERE id = @id";
                                cmd.Parameters.AddWithValue("id", var);
                                var reader = cmd.ExecuteReader();
                                account = CreateObject(reader, true);
                            } catch (Exception) {
                                scope.Dispose();
                            }
                        }
                    }
                    scope.Complete();
                } 
            }
            return account;
        }

        /*
        public Account Get(string email) {
            Account account = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "SELECT * FROM Account WHERE email = @email";
                            cmd.Parameters.AddWithValue("email", email);
                            var reader = cmd.ExecuteReader();
                            account = CreateObject(reader, true);
                        } catch (Exception) {
                            scope.Dispose();
                        }
                    }
                }
                scope.Complete();
            }
            return account;
        }
        */

        public IEnumerable<Account> GetAll() {
            IEnumerable<Account> accounts = null;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "SELECT * FROM Account";
                            var reader = cmd.ExecuteReader();
                            accounts = CreateList(reader);
                        } catch (Exception) {
                            scope.Dispose();
                        }
                    }
                }
                scope.Complete();
            }
            return accounts;
        }

        public bool Update(Account entity) {
            bool result = true;
            using (TransactionScope scope = new TransactionScope()) {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand()) {
                        try {
                            cmd.CommandText = "UPDATE Account SET name = @name, email = @email, phonenumber = @phonenumber, Password = @password WHERE id = @id";
                            cmd.Parameters.AddWithValue("id", entity.Id);
                            cmd.Parameters.AddWithValue("name", entity.Name);
                            cmd.Parameters.AddWithValue("email", entity.Email);
                            cmd.Parameters.AddWithValue("phonenumber", entity.Phone);
                            cmd.Parameters.AddWithValue("password", entity.Password);
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

        public object Delete(object var) {
            object o = null;
            if (var is string) {
                using (TransactionScope scope = new TransactionScope()) {
                    using (SqlConnection con = new SqlConnection(_connectionString)) {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand()) {
                            try {
                                cmd.CommandText = "DELETE FROM Account OUTPUT DELETED.id WHERE id = @id";
                                cmd.Parameters.AddWithValue("id", var);
                                o = cmd.ExecuteScalar();
                            } catch (Exception) {
                                o = false;
                                scope.Dispose();
                            }
                        }
                    }
                    scope.Complete();
                }
            } else {
                o = false;
            }
            return o;
        }

        /*
         * These methods below are here to create objects of type of this DB class
         */
        public static IEnumerable<Account> CreateList(SqlDataReader reader) {
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
                Id = reader.GetString(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                Phone = reader.GetString(reader.GetOrdinal("phonenumber")),
                Password = reader.GetString(reader.GetOrdinal("Password"))
            };
            return a;
        }

    }
}