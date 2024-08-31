using SalaryManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult VerifyEmail( FormCollection frm)
        {
            string Email = frm["Email"];
            AdminModel model = new AdminModel();
            bool status = model.EmailExists(Email);
            return View("VerifyEmail");
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