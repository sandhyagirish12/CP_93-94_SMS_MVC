using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;

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

        public bool AdminLogin(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string LoginQuery = "SELECT Username, Password FROM AdminData WHERE Username = '" + username + "' AND Password = '" + hashedPassword + "'";
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

        public bool EmailExists(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string existQuery = "SELECT Email FROM AdminData WHERE Email = @email";
                SqlCommand command = new SqlCommand(existQuery, connection);
                command.Parameters.AddWithValue("@email", email);
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

        public bool RegisterEmployee(List<string> employeeData)
        {
            string fname = employeeData[0];
            string lname = employeeData[1];
            string eid = employeeData[2];
            string email = employeeData[3];
            string password = employeeData[4];
            string dob = employeeData[5];
            string uname = employeeData[6];
            string gender = employeeData[7];
            string pob = employeeData[8];
            string bloodgroup = employeeData[9];
            string fathername = employeeData[10];
            string mothername = employeeData[11];
            string sname = employeeData[12];
            string dept = employeeData[13];
            string designation = employeeData[14];
            string jdate = employeeData[15];
            decimal bpay = Convert.ToDecimal(employeeData[16]);
            decimal gpay = Convert.ToDecimal(employeeData[17]);
            decimal hra = Convert.ToDecimal(employeeData[18]);
            decimal inc = Convert.ToDecimal(employeeData[19]);
            decimal da = Convert.ToDecimal(employeeData[20]);
            string accountno = employeeData[21];
            string ifsc = employeeData[22];
            string bankname = employeeData[23];
            string paddress = employeeData[24];
            string caddress = employeeData[25];
            string cnumber = employeeData[26];
            string remarks = employeeData[27];

            string hashedPassword = HashPassword(password);
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = @"INSERT INTO EmployeeData(EmployeeId, Fname, Lname, Email, Password, Gender, DOB, Username, PlaceofBirth, Bloodgroup, 
                                       Fathername, Mothername, Spousename, Joiningdate, Department, Designation, Basicpay, Gradepay, Increment, HRA, DA,
                                       Accountno, IFSC, Bankname,PAddress, CAddress,Contactno, Remarks)
                                       VALUES(
                                       @eid, @fname, @lname, @email, @pass, @gender, @dob, @uname, @pob, @bloodgroup, 
                                       @fathername, @mothername, @sname, @jdate, @dept, @designation, @bpay, @gpay, 
                                       @hra, @inc, @da, @accountno, @ifsc, @bankname,@paddress, @caddress, @cnumber, @remarks
                                        )";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@eid", eid);
                command.Parameters.AddWithValue("@fname", fname);
                command.Parameters.AddWithValue("@lname", lname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@pass", hashedPassword);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@dob", dob);
                command.Parameters.AddWithValue("@uname", uname);
                command.Parameters.AddWithValue("@pob", pob);
                command.Parameters.AddWithValue("@bloodgroup", bloodgroup);
                command.Parameters.AddWithValue("@fathername", fathername);
                command.Parameters.AddWithValue("@mothername", mothername);
                command.Parameters.AddWithValue("@sname", sname);
                command.Parameters.AddWithValue("@jdate", jdate);
                command.Parameters.AddWithValue("@dept", dept);
                command.Parameters.AddWithValue("@designation", designation);
                command.Parameters.AddWithValue("@bpay", bpay);
                command.Parameters.AddWithValue("@gpay", gpay);
                command.Parameters.AddWithValue("@hra", hra);
                command.Parameters.AddWithValue("@inc", inc);
                command.Parameters.AddWithValue("@da", da);
                command.Parameters.AddWithValue("@accountno", accountno);
                command.Parameters.AddWithValue("@ifsc", ifsc);
                command.Parameters.AddWithValue("@bankname", bankname);
                command.Parameters.AddWithValue("@paddress", paddress);
                command.Parameters.AddWithValue("@caddress", caddress);
                command.Parameters.AddWithValue("@cnumber", cnumber);
                command.Parameters.AddWithValue("@remarks", remarks);

                command.ExecuteNonQuery();
                return true;
            }
        }

        public DataTable getAllEmployee()
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT EmployeeId,Fname,Lname,Department,Designation FROM EmployeeData", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

            }
            return dt;
        }

        public DataTable EmployeeDetail(int eid)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM EmployeeData WHERE EmployeeId = @eid", connection);
                command.Parameters.AddWithValue("@eid", eid);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            return dt;
        }

        public bool UpdateEmployee(List<string> employeeData)
        {
            string fname = employeeData[0];
            string lname = employeeData[1];
            string eid = employeeData[2];
            string email = employeeData[3];
            // string password = employeeData[4];
            string dob = employeeData[5];
            string uname = employeeData[6];
            string gender = employeeData[7];
            string pob = employeeData[8];
            string bloodgroup = employeeData[9];
            string fathername = employeeData[10];
            string mothername = employeeData[11];
            string sname = employeeData[12];
            string dept = employeeData[13];
            string designation = employeeData[14];
            string jdate = employeeData[15];
            decimal bpay = Convert.ToDecimal(employeeData[16]);
            decimal gpay = Convert.ToDecimal(employeeData[17]);
            decimal hra = Convert.ToDecimal(employeeData[18]);
            decimal inc = Convert.ToDecimal(employeeData[19]);
            decimal da = Convert.ToDecimal(employeeData[20]);
            string accountno = employeeData[21];
            string ifsc = employeeData[22];
            string bankname = employeeData[23];
            string paddress = employeeData[24];
            string caddress = employeeData[25];
            string cnumber = employeeData[26];
            string remarks = employeeData[27];

            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updatequery = @"UPDATE EmployeeData SET Fname = @fname, Lname = @lname, Email = @email,
                                     Gender = @gender, DOB = @dob , Username = @uname , PlaceofBirth = @pob, 
                                     Bloodgroup = @bloodgroup,Fathername = @fathername, Mothername = @mothername,
                                     Spousename = @sname, Joiningdate = @jdate, Department = @dept,
                                     Designation = @designation, Basicpay = @bpay, Gradepay = @gpay, Increment = @inc,
                                     HRA = @hra, DA = @da, Accountno = @accountno, IFSC = @ifsc, 
                                     Bankname = @bankname,PAddress = @paddress, CAddress = @caddress,Contactno = @cnumber,
                                     Remarks = @remarks WHERE EmployeeId = @eid ";
                SqlCommand command = new SqlCommand(updatequery, connection);
                command.Parameters.AddWithValue("@eid", eid);
                command.Parameters.AddWithValue("@fname", fname);
                command.Parameters.AddWithValue("@lname", lname);
                command.Parameters.AddWithValue("@email", email);
                // command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@gender", gender);
                command.Parameters.AddWithValue("@dob", dob);
                command.Parameters.AddWithValue("@uname", uname);
                command.Parameters.AddWithValue("@pob", pob);
                command.Parameters.AddWithValue("@bloodgroup", bloodgroup);
                command.Parameters.AddWithValue("@fathername", fathername);
                command.Parameters.AddWithValue("@mothername", mothername);
                command.Parameters.AddWithValue("@sname", sname);
                command.Parameters.AddWithValue("@jdate", jdate);
                command.Parameters.AddWithValue("@dept", dept);
                command.Parameters.AddWithValue("@designation", designation);
                command.Parameters.AddWithValue("@bpay", bpay);
                command.Parameters.AddWithValue("@gpay", gpay);
                command.Parameters.AddWithValue("@hra", hra);
                command.Parameters.AddWithValue("@inc", inc);
                command.Parameters.AddWithValue("@da", da);
                command.Parameters.AddWithValue("@accountno", accountno);
                command.Parameters.AddWithValue("@ifsc", ifsc);
                command.Parameters.AddWithValue("@bankname", bankname);
                command.Parameters.AddWithValue("@paddress", paddress);
                command.Parameters.AddWithValue("@caddress", caddress);
                command.Parameters.AddWithValue("@cnumber", cnumber);
                command.Parameters.AddWithValue("@remarks", remarks);

                command.ExecuteNonQuery();
                return true;
            }
        }
        public void EmployeeDelete(int eid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM EmployeeData WHERE EmployeeId = @eid", connection);
                command.Parameters.AddWithValue("@eid", eid);
                command.ExecuteNonQuery();
            }

        }

        public bool CreateDeduction(int did, string dname, decimal percentage, decimal amount)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string insertQuery = "INSERT INTO Deduction(DeductionId, Dname, Percentage, Amount) VALUES(@did, @dname, @percentage, @amount)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@did", did);
                command.Parameters.AddWithValue("@dname", dname);
                command.Parameters.AddWithValue("@percentage", percentage);
                command.Parameters.AddWithValue("@amount", amount);
                command.ExecuteNonQuery();
                return true;
            }
        }
        
        public DataTable GetAllDeduction()
        {
            DataTable dt=new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getQuery = "SELECT * FROM Deduction";
                SqlCommand command = new SqlCommand(getQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetDeduction(int did)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getQuery = "SELECT * FROM Deduction WHERE DeductionId = @did";
                SqlCommand command = new SqlCommand(getQuery, connection);
                command.Parameters.AddWithValue("@did", did);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            return dt;
        }
        public  void UpdateDeduction(int did, string dname, decimal amount, decimal percentage)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Deduction SET Dname = @dname, Amount = @amount, Percentage = @percentage WHERE DeductionId = @did";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@dname", dname);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@percentage", percentage);
                command.Parameters.AddWithValue("@did", did);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteDeduction(int did)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Deduction WHERE DeductionId = @did";
                SqlCommand command = new SqlCommand(deleteQuery,connection);
                command.Parameters.AddWithValue("@did", did);
                command.ExecuteNonQuery();
               
            }
        }
    }
}


   
