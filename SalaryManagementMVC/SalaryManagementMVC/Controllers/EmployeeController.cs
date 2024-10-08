﻿using SalaryManagementMVC.Models;
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
            string username = Session["Username"].ToString();
            EmployeeModel model = new EmployeeModel();
            model.CreateLeave(username, ltype, fromdate, todate, description);
            return View("Leave");
        }
        public ActionResult Leave()
        {
            return View("Leave");
        }
        public ActionResult CreateLoan(FormCollection frm)
        {
            string loannumber = frm["loannumber"].ToString();
            string loanType = frm["loanType"].ToString();
            string bankName = frm["bankName"].ToString();
            string ifscCode = frm["ifscCode"].ToString();
            decimal totalAmount = Convert.ToDecimal(frm["totalAmount"]);
            decimal monthlyPayment = Convert.ToDecimal(frm["monthlyPayment"]);
            string startingDate = frm["startingDate"].ToString();
            int tenure = Convert.ToInt32(frm["tenure"]);
            string description = frm["description"].ToString();
            string username = Session["Username"].ToString();
            EmployeeModel model = new EmployeeModel();
            model.CreateLoan(username,loannumber, loanType, bankName, ifscCode, totalAmount, monthlyPayment, startingDate, tenure, description);
            return View("Loan");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Home");
        }
    }
}