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

        public ActionResult Register()
        {
            return View("Registration");
        }

        public ActionResult AdminRegister(FormCollection form, string action)
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

        public ActionResult VerifyEmail()
        {
            return View("VerifyEmail");
        }

        public ActionResult ResetPassword()
        {
            return View("ResetPassword");
        }

        public ActionResult Dashboard()
        {
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
            return View("Home");
        }
    }
}