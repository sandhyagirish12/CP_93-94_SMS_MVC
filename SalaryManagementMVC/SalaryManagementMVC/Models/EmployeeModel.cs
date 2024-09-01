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
    public class EmployeeModel
    {
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

        public bool EmployeeLogin(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string LoginQuery = "SELECT Username, Password FROM EmployeeData WHERE Username = '" + username + "' AND Password = '" + hashedPassword + "'";
                SqlCommand command = new SqlCommand(LoginQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
    }
}