using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RESTServices.Database {
    public class DatabaseConnection {
        public string connectionString { get; set; } = "data source=hildur.ucn.dk; Database=dmaa0919_1080638; user=dmaa0919_1080638; password=Password1!";

        public void SampleMethod() {
            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                Console.WriteLine(con.State);
                Console.WriteLine(con.ConnectionString);
                Console.WriteLine(con.DataSource);
                Console.WriteLine(con.Database);
                string queryString = "SELECT * FROM Accounts"; // QUERY
                using (SqlCommand createCommand = new SqlCommand(queryString, con)) {
                    SqlDataReader data = createCommand.ExecuteReader();
                    while (data.Read()) {
                        Console.WriteLine(data.GetString(1));
                    }
                }
                con.Close();
            }
        }
    }
}