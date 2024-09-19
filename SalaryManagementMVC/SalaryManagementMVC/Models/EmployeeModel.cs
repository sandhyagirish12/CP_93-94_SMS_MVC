using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

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
        public void CreateLoan(string username,string loannumber, string loanType, string bankName,  string ifscCode, decimal totalAmount, decimal monthlyPayment, string startingDate, int tenure, string description)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getidquery = "SELECT EmployeeId FROM EmployeeData WHERE Username=@username";
                SqlCommand command = new SqlCommand(getidquery, connection);
                command.Parameters.AddWithValue("@username", username);
                string employeeId = command.ExecuteScalar().ToString();

                string insertQuery = @"INSERT INTO LoanDetails(EmployeeId, LoanNo, LType, BankName, IFSC, Installment, StartDate, Tenure, Description)
                                     VALUES (@employeeId, @LoanNo, @LType, @BankName, @IFSC, @Installment, @StartDate, @Tenure,@Description)";
                SqlCommand cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.Parameters.AddWithValue("@LoanNo", loannumber);
                cmd.Parameters.AddWithValue("@LType", loanType);
                cmd.Parameters.AddWithValue("@BankName", bankName);
                cmd.Parameters.AddWithValue("@IFSC", ifscCode);
                cmd.Parameters.AddWithValue("@Installment", monthlyPayment);
                cmd.Parameters.AddWithValue("@StartDate", startingDate);
                cmd.Parameters.AddWithValue("@Tenure", tenure);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.ExecuteNonQuery();

            }
        }
        public void CreateLeave(string ltype, string fromdate, string todate, string description)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO LeaveDetails(LType, FromDate, Todate, Description) VALUES (@ltype, @fromdate, @todate, @description)";
                SqlCommand cmd= new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@ltype", ltype);
                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.ExecuteNonQuery();

            }
        }
    }
}