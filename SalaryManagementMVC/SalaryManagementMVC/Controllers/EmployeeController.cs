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

        public ActionResult VerifyEmail()
        {
            return View("VerifyEmail");
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

        public ActionResult Leave()
        {
            return View("Leave");
        }
    }
}