using SalaryManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public ActionResult VerifyEmail(FormCollection frm)
        {
            string Email = frm["Email"];
            AdminModel model = new AdminModel();
            bool status = model.EmailExists(Email);

            if (status)
            {
                Random random = new Random();
                int otp = random.Next(1000, 9999);
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
            var employeeData = new List<string>
            {
                frm["fname"],
                frm["lname"],
                frm["eid"],
                frm["email"],
                frm["password"],
                frm["dob"],
                frm["uname"],
                frm["gender"],
                frm["pob"],
                frm["bloodgroup"],
                frm["fathername"],
                frm["mothername"],
                frm["sname"],
                frm["dept"],
                frm["designation"],
                frm["jdate"],
                frm["bpay"],
                frm["gpay"],
                frm["hra"],
                frm["inc"],
                frm["da"],
                frm["accountno"],
                frm["ifsc"],
                frm["bankname"],
                frm["paddress"],
                frm["caddress"],
                frm["contact"],
                frm["remarks"],
            };

            // Create an instance of the model class
            AdminModel model = new AdminModel();

            // Pass the list to the model class method
            bool isRegistered = model.RegisterEmployee(employeeData);
            if(isRegistered)
            {
                ViewBag.Message = "Successfully Registered";
                return View("CreateEmployee");
            }
            else
            {
                return View("CreateEmployee");
            }
            
        }

        public ActionResult ViewEmployee()
        {
            AdminModel model = new AdminModel();
            DataTable dt = model.getAllEmployee();

            return View("ViewEmployee",dt);
        }

        public ActionResult DeleteEmployee(int eid)
        {
            AdminModel model=new AdminModel();
            model.EmployeeDelete(eid);
            return RedirectToAction("ViewEmployee");
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