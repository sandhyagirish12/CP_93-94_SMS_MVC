using SalaryManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
            if (isRegistered)
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
            DataTable dt = model.GetAllEmployee();

            return View("ViewEmployee", dt);
        }

        public ActionResult EditEmployee(string eid)
        {
            DataTable dt = new DataTable();
            AdminModel model = new AdminModel();
            dt = model.EmployeeDetail(eid);

            // Check if the DataTable has any rows
            if (dt.Rows.Count > 0)
            {
                ViewBag.EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
                ViewBag.Username = dt.Rows[0]["Username"].ToString();
                ViewBag.FirstName = dt.Rows[0]["FName"].ToString();
                ViewBag.LastName = dt.Rows[0]["LName"].ToString();
                ViewBag.Email = dt.Rows[0]["Email"].ToString();
                ViewBag.Password = dt.Rows[0]["Password"].ToString();
                ViewBag.DateOfBirth = dt.Rows[0]["DOB"].ToString();
                ViewBag.Gender = dt.Rows[0]["Gender"].ToString();
                ViewBag.PlaceOfBirth = dt.Rows[0]["PlaceOfBirth"].ToString();
                ViewBag.BloodGroup = dt.Rows[0]["Bloodgroup"].ToString();
                ViewBag.FatherName = dt.Rows[0]["Fathername"].ToString();
                ViewBag.MotherName = dt.Rows[0]["Mothername"].ToString();
                ViewBag.SpouseName = dt.Rows[0]["Spousename"].ToString();
                ViewBag.Department = dt.Rows[0]["Department"].ToString();
                ViewBag.Designation = dt.Rows[0]["Designation"].ToString();
                ViewBag.JoiningDate = dt.Rows[0]["Joiningdate"].ToString();
                ViewBag.BasicPay = Convert.ToDecimal(dt.Rows[0]["Basicpay"]);
                ViewBag.GradePay = Convert.ToDecimal(dt.Rows[0]["Gradepay"]);
                ViewBag.HRA = Convert.ToDecimal(dt.Rows[0]["HRA"]);
                ViewBag.Increment = Convert.ToDecimal(dt.Rows[0]["Increment"]);
                ViewBag.DA = Convert.ToDecimal(dt.Rows[0]["DA"]);
                ViewBag.AccountNo = dt.Rows[0]["Accountno"].ToString();
                ViewBag.IFSC = dt.Rows[0]["IFSC"].ToString();
                ViewBag.BankName = dt.Rows[0]["Bankname"].ToString();
                ViewBag.PermanentAddress = dt.Rows[0]["PAddress"].ToString();
                ViewBag.CurrentAddress = dt.Rows[0]["CAddress"].ToString();
                ViewBag.ContactNo = dt.Rows[0]["Contactno"].ToString();
                ViewBag.Remarks = dt.Rows[0]["Remarks"].ToString();
            }
            return View("EditEmployee");
        }
        public ActionResult UpdateEmployee(FormCollection frm)
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
            bool isUpdated = model.UpdateEmployee(employeeData);
            if (isUpdated)
            {
                ViewBag.Message = "Successfully Updated";
                return View("ViewEmployee");
            }
            else
            {
                return View("ViewEmployee");
            }
        }
        public ActionResult DeleteEmployee(int eid)
        {
            AdminModel model = new AdminModel();
            model.EmployeeDelete(eid);
            return RedirectToAction("ViewEmployee");
        }

        public ActionResult ManagePayroll()
        {
            AdminModel model = new AdminModel();
            DataTable dt = model.GetAllEmployee();
            List<string> employeeIds = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    employeeIds.Add(row["employeeId"].ToString());
                }
            }

            // Pass the list of employee IDs to the view
            ViewBag.EmployeeIds = employeeIds;
            return View("Payroll");
        }

        public JsonResult GetEmployeeDetails(string employeeId)
        {
            AdminModel model = new AdminModel();
            DataTable dt = model.EmployeeDetail(employeeId);

            if (dt.Rows.Count > 0)
            {
                var employeeData = new
                {
                    EmployeeName = dt.Rows[0]["Fname"].ToString() + " " + dt.Rows[0]["Lname"].ToString(),
                    Designation = dt.Rows[0]["Designation"].ToString(),
                    Department = dt.Rows[0]["Department"].ToString(),
                    BasicPay = dt.Rows[0]["Basicpay"].ToString(),
                    AccountNo = dt.Rows[0]["Accountno"].ToString(),
                    Increment = dt.Rows[0]["Increment"].ToString(),
                    HRA = dt.Rows[0]["HRA"].ToString(),
                    DA = dt.Rows[0]["DA"].ToString(),
                    MaxLeave = 10 // Set maximum leave to 10

                };
                return Json(employeeData, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetLeaveDetails(string employeeId, int selectedYear, int selectedMonth,float basicpay)
        {
            AdminModel model = new AdminModel();
            DataTable dt = model.GetApprovedLeaves(employeeId, selectedYear, selectedMonth);

            int approvedLeaves = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime fromDate = Convert.ToDateTime(row["FromDate"]);
                    DateTime toDate = Convert.ToDateTime(row["ToDate"]);
                    approvedLeaves += (toDate - fromDate).Days + 1; // Include both fromDate and toDate
                }
            }

            // Calculate leave deduction
            int maxLeave = 10;
            int excessLeave = Math.Max(0, approvedLeaves - maxLeave);
            float leaveDeduction = excessLeave *(basicpay/30); 

            var leaveDetails = new
            {
                ApprovedLeave = approvedLeaves,
                LeaveDeduction = leaveDeduction
            };

            return Json(leaveDetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayrollSubmit(FormCollection frm)
        {
           // var employeeId = frm["EmployeeId"];
           // var month = frm["Month"];
          //  var year = frm["Year"];
            //var basicPay = Convert.ToDecimal(frm["BasicPay"]);
            //var hraPercent = Convert.ToDecimal(frm["HRA"]); // HRA in percentage
            //var daPercent = Convert.ToDecimal(frm["DA"]);  // DA in percentage
            //var incrementPercent = Convert.ToDecimal(frm["Increment"]); // Increment in percentage

            // Perform the calculations
            //var hra = (basicPay * hraPercent) / 100;
            //var da = (basicPay * daPercent) / 100;
            //var increment = (basicPay * incrementPercent) / 100;
            //var salary = basicPay + hra + da + increment;

            var employeeData = new List<string>
            {
                frm["EmployeeId"],
                frm["Month"],
                frm["Year"],
                frm["salary"],
                //frm["Basicpay"],
                //frm["Increment"],
                //frm["HRA"],
                //frm["DA"]
            };

            AdminModel model = new AdminModel();

            // Pass the list to the model class method
            bool isUpdated = model.PayrollRegister(employeeData);
            if (isUpdated)
            {
                ViewBag.Message = "Successfully Updated";

            }
                return RedirectToAction("ManagePayroll");
           
            
        }
        public ActionResult ManageLoan()
        {
            AdminModel model = new AdminModel();
            DataTable dt = model.GetAllLoan();
            return View("Loan", dt);
        }

        public ActionResult LoanStatusUpdate(int Lid,string status)
        {
            AdminModel model = new AdminModel();
            model.LoanStatus(Lid,status);
            return RedirectToAction("ManageLoan");

        }

        public ActionResult ManageLeave()
        {

            AdminModel model = new AdminModel();
            DataTable dt = model.GetLeaveDetails();
            return View("Leave", dt);
        }
        public ActionResult LeaveStatusUpdate(int Leaveid, string status)
        {
            AdminModel model = new AdminModel();
            model.LeaveStatus(Leaveid, status);
            return RedirectToAction("ManageLeave");

        }


        public ActionResult CreateDeduction()
        {
            AdminModel model = new AdminModel();
            DataTable dt = model.GetAllDeduction();
            return View("CreateDeduction", dt);
        }
       
        public ActionResult RegisterDeduction(FormCollection form)
        {
            AdminModel model = new AdminModel();
            int Deductionid = Convert.ToInt32(form["did"]);
            string Deductionname = form["dname"];
            decimal Percentage = Convert.ToDecimal(form["percentage"]);
            decimal Amount = Convert.ToDecimal(form["amount"]);
            bool isCreateDeduction = model.CreateDeduction(Deductionid, Deductionname, Percentage, Amount);
            ViewBag.Message = "Successfully Registered";
            return RedirectToAction("CreateDeduction");
        }

        public ActionResult EditDeduction(int did)
        {

            DataTable dt = new DataTable();
            AdminModel model = new AdminModel();
            dt = model.GetDeduction(did);

            // Check if the DataTable has any rows
            if (dt.Rows.Count > 0)
            {
                ViewBag.DeductionId=dt.Rows[0]["DeductionId"];
                ViewBag.Dname = dt.Rows[0]["Dname"];
                ViewBag.Percentage = dt.Rows[0]["Percentage"];
                ViewBag.Amount = dt.Rows[0]["Amount"];
            }
            return View("EditDeduction");
        }

        public  ActionResult UpdateDeduction(FormCollection frm)
        {
            int did = Convert.ToInt32(frm["did"]);
            string dname = frm["dname"].ToString();
            decimal amount = Convert.ToDecimal(frm["amount"]);
            decimal percentage = Convert.ToDecimal(frm["percentage"]);
            AdminModel model=new AdminModel();
            model.UpdateDeduction(did,dname,amount,percentage);
            return RedirectToAction("CreateDeduction");
        }
        public ActionResult DeleteDeduction(int did)
        {
            AdminModel  model=new AdminModel();
            model.DeleteDeduction(did);
            return RedirectToAction("CreateDeduction");
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