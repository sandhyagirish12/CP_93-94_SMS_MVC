using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SalaryManagementMVC.Models
{
    public class AdminModel
    {
        public bool AdminRegister(string fname, string lname, string uname, string email, string pass)
        {
            string hashedPassword = HashPassword(pass);
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                //check admin count greater than 5 or not
                string countQuery = "SELECT COUNT(*) FROM AdminData";
                SqlCommand cmd = new SqlCommand(countQuery, connection);
                int adminCount = (int)cmd.ExecuteScalar();

                if (adminCount >= 5)
                {
                    return false;
                }

                string insertQuery = "INSERT INTO AdminData(Fname, Lname, Username, Password, Email) VALUES(@fname, @lname, @username, @password, @email)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@fname", fname);
                command.Parameters.AddWithValue("@lname", lname);
                command.Parameters.AddWithValue("@username", uname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.ExecuteNonQuery();
                return true;
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Compute the hash as a byte array
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the hash to a Base64 string and return it
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}


   
