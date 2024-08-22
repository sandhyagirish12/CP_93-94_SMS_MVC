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

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        public ActionResult CreateEmployee()
        {
            return View("CreateEmployee");
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

        public ActionResult ManageDepartment()
        {
            return View("Department");
        }

        public ActionResult ManageAllowance()
        {
            return View("Allowance");
        }
    }
}