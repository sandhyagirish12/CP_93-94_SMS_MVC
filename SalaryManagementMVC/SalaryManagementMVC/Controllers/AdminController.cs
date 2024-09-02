﻿using SalaryManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SalaryManagementMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("Home");

        }
        public ActionResult AdminLogin(FormCollection form)
        {
            AdminModel model = new AdminModel();
            string username = form["uname"];
            string password = form["password"];
            bool loginstatus = model.AdminLogin(username, password);
            if (loginstatus)
            {
                Session["Username"] = username;
                return View("Dashboard");
            }
            else
            {
                ViewBag.Message = "Invalid username or password. Please try again.";
                return View("Home");
            }
        }
        
        public ActionResult Register()
        {
            return View("Registration");
        }

        public ActionResult AdminRegister(FormCollection form, string action)
        {
            if (action == "submit")
            {
                AdminModel model = new AdminModel();
                string Fname = form["txtFname"];
                string Lname = form["txtLname"];
                string Username = form["txtUname"];
                string Email = form["txtEmail"];
                string Password = form["txtPass"];
                bool isRegistered = model.AdminRegister(Fname, Lname, Username, Email, Password);

                if (!isRegistered)
                {
                    ViewBag.Message = "The maximum number of 5 admins has been reached. Cannot register more admins.";
                    return View("Registration");
                }
                ViewBag.Message = "Successfully Registered";
                return View("Registration");
            }
            else
            {
                return View("Registration");
            }
        }

        public ActionResult ConfirmEmail()
        {
            return View("VerifyEmail");
        }

        public ActionResult VerifyEmail( FormCollection frm)
        {
            string Email = frm["Email"];
            AdminModel model = new AdminModel();
            bool status = model.EmailExists(Email);

            if(status)
            {
                Random random = new Random();
                int otp = random.Next(1000,9999);
                Session["otp"] = otp;
                Session["email"] = Email;
                SendToEmail(otp, Email);
                return View("ResetPassword");
            }
            return View("Home");
        }
 
        public void SendToEmail(int otp, string Email)
        {
                        
                MailMessage message = new MailMessage();
                message.To.Add(Email);
                message.From = new MailAddress("salarymanagementmvc@gmail.com");
                message.Subject = "Reset OTP";
                message.Body = $"Your OTP is: {otp}";
                message.IsBodyHtml = true; // This allows the email body to be formatted as HTML
                
              
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
                smtp.Credentials = new System.Net.NetworkCredential("salarymanagementmvc@gmail.com", "Mvc$project!452$01$!!");
                smtp.EnableSsl = true;

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    throw new InvalidOperationException("Failed to send email.", ex);
                }
            

        }

        public ActionResult ResetPassword()
        {
            return View("ResetPassword");
        }

        public ActionResult Dashboard()
        {
            // Retrieve the username from session
            string username = Session["Username"] as string;

            // Use the username as needed in your controller logic
            ViewBag.Username = username;
            return View("Dashboard");
        }

        
        public ActionResult CreateEmployee()
        {
            return View("CreateEmployee");
        }

        public ActionResult RegisterEmployee(FormCollection frm)
        {
            string fname = frm["fname"];
            string lname = frm["lname"];
            string eid = frm["eid"];
            string email = frm["email"];
            string password = frm["password"];
            string dob = frm["dob"];
            int age = Convert.ToInt32(frm["age"]);
            string gender = frm["gender"];
            string pob = frm["pob"];
            string bloodgroup = frm["bloodgroup"];
            string fathername = frm["fathername"];
            string mothername = frm["mothername"];
            string sname = frm["sname"];
            string dept = frm["dept"];
            string jdate = frm["jdate"];
            string designation = frm["designation"];
            decimal bpay = Convert.ToDecimal(frm["bpay"]);
            decimal gpay = Convert.ToDecimal(frm["gpay"]);
            decimal hra = Convert.ToDecimal(frm["hra"]);
            decimal inc =Convert.ToDecimal(frm["inc"]);
            decimal da =Convert.ToDecimal(frm["da"]);
            string ifsc = frm["ifsc"];
            string bankname = frm["bankname"];
            string paddress = frm["paddress"];
            string caadress = frm["caddress"];
            int cnumber =Convert.ToInt32(frm["cnumber"]);
            string remarks = frm["remarks"];
           
           
            return View("");
            
        }

        public class EmployeeRegistration
        {
            public string Fname { get; set; }
            public string Lname { get; set; }
            public string Eid { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Dob { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public string Pob { get; set; }
            public string Bloodgroup { get; set; }
            public string Fathername { get; set; }
            public string Mothername { get; set; }
            public string Sname { get; set; }
            public string Dept { get; set; }
            public string Jdate { get; set; }
            public string Designation { get; set; }
            public decimal Bpay { get; set; }
            public decimal Gpay { get; set; }
            public decimal Hra { get; set; }
            public decimal Inc { get; set; }
            public decimal Da { get; set; }
            public string Ifsc { get; set; }
            public string Bankname { get; set; }
            public string Paddress { get; set; }
            public string Caddress { get; set; }
            public int Cnumber { get; set; }
            public string Remarks { get; set; }
            public List<string> Skills { get; set; } // Optional: Use an array if you prefer
        }
        public ActionResult ViewEmployee()
        {
            return View("ViewEmployee");
        }

        public ActionResult ManagePayroll()
        {
            return View("Payroll");
        }

        public ActionResult ManageLoan()
        {
            return View("Loan");
        }

        public ActionResult ManageLeave()
        {
            return View("Leave");
        }

        public ActionResult ManageDeduction()
        {
            return View("Deduction");
        }

        //public ActionResult ManageDepartment()
        //{
        //    return View("Department");
        //}

        //public ActionResult ManageAllowance()
        //{
        //    return View("Allowance");
        //}

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Home");
        }
    }
}