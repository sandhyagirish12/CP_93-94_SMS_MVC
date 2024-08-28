using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SalaryManagementMVC.Models
{
    public class AdminModel
    {
        public void AdminRegister(string fname, string lname, string uname, string email, string pass)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO AdminData(Fname, Lname, Username, Password, Email) VALUES(@fname, @lname, @username, @password, @email)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@fname", fname);
                command.Parameters.AddWithValue("@lname", lname);
                command.Parameters.AddWithValue("@username", uname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", pass);
                command.ExecuteNonQuery();
                return;
            }
        }
    }
}