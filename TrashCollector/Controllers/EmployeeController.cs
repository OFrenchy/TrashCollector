using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;


namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext db;

        public EmployeeController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Employee; 
        // get customers with the same zip; 
        // default view is today, get today's customers, 
        // less any people who are set to skip, 
        // plus any special pickups
        public ActionResult Index(int? selectDayOfWeek)
        {
            //Empemp1!@gmail.com

            //document.getElementById( )


            // Build the list of days
            List<SelectListItem> DaysOfWeek = new List<SelectListItem>();
            DayOfWeek dow = DayOfWeek.Monday;
            for (int i = 1; i < 6; i++)
            {
                DaysOfWeek.Add((new SelectListItem() { Text = dow.ToString(), Value = i.ToString() }));
                dow++;
            }
            //DaysOfWeek.Add((new SelectListItem() { Text = "*", Value = "*" }));
            ViewBag.DaysOfWeek = DaysOfWeek;
            ViewBag.Day = DateTime.Today.DayOfWeek.ToString();
            
            var appUserID = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationUserId == appUserID).First();
            //var customers; //= db.Employees.FirstOrDefault();

            //Empemp1!@gmail.com

            // if null, default to today;  apply all the filters
            if (selectDayOfWeek == null)
            {
                // LINQ didn't recognize the ToInt32
                int dayOfWeek = Convert.ToInt32(DateTime.Today.DayOfWeek);
                var customers = db.Customers.Where
                    (w =>
                        (
                            (w.Zip == db.Employees.Where(e => e.ID == employee.ID).FirstOrDefault().Zip) &&
                            (w.DayOfWeekPickup == dayOfWeek) &&
                            (
                                (w.StopDate != null ? DateTime.Today < w.StopDate : true) ||
                                (w.StartDate != null ? DateTime.Today >= w.StartDate : true)
                            )
                        ) 
                        ||
                        w.SpecialPickupDate == DateTime.Today
                    ).ToList();
                return View(customers);
            }
            else
                // show all of the selected days' pickups;  since it's not today, show all;  
                // the customer could always go online & change something between now & then anyway
            {
                var customers = db.Customers.Where
                (w =>
                    (w.Zip == db.Employees.Where(e => e.ID == employee.ID).FirstOrDefault().Zip) &&
                    (w.DayOfWeekPickup == selectDayOfWeek)
                ).ToList();
                return View(customers);
            }
            //return View(customers);
        } // index

        //FilterDayOfWeek(int selectDayOfWeek)
        public ActionResult MapAllAddresses(int? selectDayOfWeek)
        {
            var appUserID = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationUserId == appUserID).First();
            // https://www.google.com/maps/search/?api=1&query=centurylink+field
            // https://www.google.com/maps/place/1817+Spruce+Ct,+South+Milwaukee,+WI  : works too 

            //Empemp1!@gmail.com

            if (selectDayOfWeek == null) selectDayOfWeek = Convert.ToInt32( DateTime.Today.DayOfWeek);

            //var customers = db.Customers.Where(w => w.DayOfWeekPickup == selectDayOfWeek && w.Zip == employee.Zip);
            var customers = db.Customers.Where
                    (w =>
                        (
                            (w.Zip == db.Employees.Where(e => e.ID == employee.ID).FirstOrDefault().Zip) &&
                            (w.DayOfWeekPickup == selectDayOfWeek) &&
                            (
                                (w.StopDate != null ? DateTime.Today < w.StopDate : true) ||
                                (w.StartDate != null ? DateTime.Today >= w.StartDate : true)
                            )
                        )
                        ||
                        w.SpecialPickupDate == DateTime.Today
                    ).ToList();


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("https://www.google.com/maps/search/?api=1&query=");
            foreach (Customer customer in customers)
            {
                stringBuilder.Append(customer.Street.Trim().Replace(" ", "+"));
                stringBuilder.Append(",");
                stringBuilder.Append(customer.City.Trim().Replace(" ", "+"));
                stringBuilder.Append(",");
                stringBuilder.Append(customer.State.Trim().Replace(" ", "+"));
                stringBuilder.Append(";");
            }

            Process.Start(stringBuilder.ToString());

            // convert address to lat/long
            //https://maps.googleapis.com/maps/api/geocode/json?parameters
            //stringBuilder.Clear();
            //stringBuilder.Append("https://maps.googleapis.com/maps/api/geocode/json?parameters");
            //stringBuilder.Append(customer.Street.Trim().Replace(" ", "+"));
            //stringBuilder.Append(",");
            //stringBuilder.Append(customer.City.Trim().Replace(" ", "+"));
            //stringBuilder.Append(",");
            //stringBuilder.Append(customer.State.Trim().Replace(" ", "+"));
            return RedirectToAction("Index", "Employee");
            //return null;
        } // MapAllAddresses
        public ActionResult MapAddress(int? id)
        {
            // https://www.google.com/maps/search/?api=1&query=centurylink+field
            // https://www.google.com/maps/place/1817+Spruce+Ct,+South+Milwaukee,+WI  : works too 
            
            //Empemp1!@gmail.com
            Customer customer = db.Customers.Find(id);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("https://www.google.com/maps/search/?api=1&query=");
            stringBuilder.Append(customer.Street.Trim().Replace(" ", "+"));
            stringBuilder.Append(",");
            stringBuilder.Append(customer.City.Trim().Replace(" ", "+"));
            stringBuilder.Append(",");
            stringBuilder.Append(customer.State.Trim().Replace(" ", "+"));

            Process.Start(stringBuilder.ToString());

            // convert address to lat/long
            //https://maps.googleapis.com/maps/api/geocode/json?parameters
            //stringBuilder.Clear();
            //stringBuilder.Append("https://maps.googleapis.com/maps/api/geocode/json?parameters");
            //stringBuilder.Append(customer.Street.Trim().Replace(" ", "+"));
            //stringBuilder.Append(",");
            //stringBuilder.Append(customer.City.Trim().Replace(" ", "+"));
            //stringBuilder.Append(",");
            //stringBuilder.Append(customer.State.Trim().Replace(" ", "+"));
            return RedirectToAction("Index", "Employee");
            //return null;
        } // MapAddress

        // FilterDayOfWeek
        public ActionResult FilterDayOfWeek(int selectDayOfWeek)
        {
            // Return ALL customers with dayofweek pickup == dayOfWeek
            var appUserID = User.Identity.GetUserId();
            var employee = db.Employees.Where(e => e.ApplicationUserId == appUserID).First();

            //Empemp1!@gmail.com

            var customers = db.Customers.Where
                (w =>
                    (w.Zip == db.Employees.Where(e => e.ID == employee.ID).FirstOrDefault().Zip) &&
                    (w.DayOfWeekPickup == selectDayOfWeek) 
                ).ToList();

            // Build the list of days
            List<SelectListItem> DaysOfWeek = new List<SelectListItem>();
            DayOfWeek dow = DayOfWeek.Monday;
            for (int i = 1; i < 6; i++)
            {
                DaysOfWeek.Add((new SelectListItem() { Text = dow.ToString(), Value = i.ToString() }));
                dow++;
            }
            //DaysOfWeek.Add((new SelectListItem() { Text = "*", Value = "*" }));
            ViewBag.DaysOfWeek = DaysOfWeek;
            ViewBag.Day = DateTime.Today.DayOfWeek.ToString();
            return View("Index", customers);

            //return RedirectToAction("Index", "Employee");
            
            //return View(customers);
        }
        // ConfirmPickup
        public ActionResult ConfirmPickup(int id)
        {
            // id is of customer to confirm pickup
            try
            {
                //Empemp1!@gmail.com
                Customer customer = db.Customers.Find(id);
                // Only bill them if it's not their free pickup
                int amountDue = 25;
                if (DateTime.Today == (customer.SpecialPickupDate ?? DateTime.Today.AddDays(-1)))  amountDue = 0; 
                customer.Bill = customer.Bill + amountDue;
                customer.BillDetails = $"{(customer.BillDetails ?? "")}{DateTime.Today.Month.ToString()}/{DateTime.Today.Day.ToString()} ${amountDue}; ";
                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            catch
            {
                return RedirectToAction("Index", "Employee");
            }
        }
        
        // GET: Employee/Confirm/5
        public ActionResult Confirm(int id)
        {
            // id is of customer to confirm pickup
            try
            {
                Customer customer = db.Customers.Find(id);

                return View(customer);


                // STOP:  put this in the HTTPPOST??

                customer.Bill = customer.Bill + 25;
                //Empemp1!@gmail.com
                customer.BillDetails = customer.BillDetails ?? "" + DateTime.Today.Month.ToString() + "/" +
                    DateTime.Today.Day.ToString() + " $25; ";
                db.SaveChanges();

                //int thisUserID = db.Employees.Where(w => w.ApplicationUser.Email == model.Email).SingleOrDefault().ID;
                //ViewBag.EmployeeID = thisUserID;
                int thisUserID = ViewBag.EmployeeID;
                // STOP - ??? Do I need to include the employee ID?  
                return RedirectToAction("Index", "Employee", new { id = thisUserID });

                // ??? Why is it the following in the AccountController?
                //return RedirectToAction("Index", "Employee", new { id = thisUserID });
            }
            catch
            {
                // STOP - ??? where to send them if the above fails?
                return View();
            }


            // return to index!!!
            return View(db.Employees.Find(id));
        }

        //// POST: Employee/Index = confirm pickup
        //[HttpPost]
        //public ActionResult Index(int id, int employeeID)
        //{
        //    // We are confirming a pickup today for the customer id
        //    try
        //    {
        //        Customer customer  = db.Customers.Find(id);
        //        customer.Bill = customer.Bill + 25;
        //        //Empemp1!@gmail.com
        //        customer.BillDetails = customer.BillDetails ?? "" + DateTime.Today.Month.ToString() + "/" + 
        //            DateTime.Today.Day.ToString() + " $25; ";
        //        db.SaveChanges();

        //        //int thisUserID = db.Employees.Where(w => w.ApplicationUser.Email == model.Email).SingleOrDefault().ID;
        //        //ViewBag.EmployeeID = thisUserID;
        //        int thisUserID = ViewBag.EmployeeID;
        //        return RedirectToAction("Index", "Employee", new { id = thisUserID });
        //    }
        //    catch
        //    {
        //        // STOP - where to send them if the above fails?
        //        return View();
        //    }

        //}

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Employees.Find(id));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                employee.RoleName = "Employee";
                // gets the id of the currently logged in ASPNetUser
                employee.ApplicationUserId = User.Identity.GetUserId();

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = employee.ID });
            }
            catch
            {
                return View(employee);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Employees.Find(id));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                Employee thisEmployee = db.Employees.Find(id);
                thisEmployee.Zip = employee.Zip;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(employee);
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                db.Employees.Remove(db.Employees.Find(id));
                db.SaveChanges();

                // STOP - send them to somewhere that will work;  
                // Index requires an employee ID as integer
                return RedirectToAction("Index");
            }
            catch
            {
                return View(employee);
            }
        }
    }
}
