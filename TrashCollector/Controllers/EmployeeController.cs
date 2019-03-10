using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index(int id)
        {
            // LINQ didn't recognize the ToInt32
            int dayOfWeek = Convert.ToInt32(DateTime.Today.DayOfWeek);
            var customers = db.Customers.Where
                (w =>
                    (
                        (w.Zip == db.Employees.Where(e => e.ID == id).FirstOrDefault().Zip) &&
                        (w.DayOfWeekPickup == dayOfWeek) &&
                        (
                            (w.StartDate != null ? DateTime.Today < w.StartDate : true) ||
                            (w.StopDate != null ? DateTime.Today >= w.StopDate : true)
                        )
                    ) 
                    ||
                    w.SpecialPickupDate == DateTime.Today
                ).ToList();

            // Build the list of days
            if (!ViewBag.EmployeeDaysOfWeek)
            {
                List<SelectListItem> EmployeeDaysOfWeek = new List<SelectListItem>();
                DayOfWeek dow = DayOfWeek.Monday;
                for (int i = 1; i < 6; i++)
                {
                    EmployeeDaysOfWeek.Add((new SelectListItem() { Text = dow.ToString(), Value = i.ToString() }));
                    dow++;
                }
                EmployeeDaysOfWeek.Add((new SelectListItem() { Text = "*", Value = "*" }));
                ViewBag.EmployeeDaysOfWeek = EmployeeDaysOfWeek;
            }
            return View(customers);
        }


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
                return RedirectToAction("Index");
            }
            catch
            {
                return View(employee);
            }
        }
    }
}
