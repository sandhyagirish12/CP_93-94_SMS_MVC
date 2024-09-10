using SalaryManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalaryManagementMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult EmployeeLogin(FormCollection frm)
        {

            EmployeeModel model = new EmployeeModel();
            string username = frm["uname"];
            string password = frm["password"];
            bool loginstatus = model.EmployeeLogin(username, password);
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

        public ActionResult Payroll()
        {
            return View("Payroll");
        }

        public ActionResult Loan()
        {
            return View("Loan");
        }

        public ActionResult RegisterLeave(FormCollection frm)
        {
            string ltype = frm["leaveType"].ToString();
            string fromdate = frm["fromDate"].ToString();
            string todate = frm["toDate"].ToString();
            string description = frm["description"].ToString();
            EmployeeModel model = new EmployeeModel();
            model.CreateLeave(ltype, fromdate, todate, description);
            return View("RegisterLeave");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Home");
        }
    }
}