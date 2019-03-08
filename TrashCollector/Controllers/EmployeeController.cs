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

        // GET: Employee
        //public ActionResult Index()
        //{
        //    //return View(db.SuperHeroes.OrderBy(o => o.Name).ToList());
        //    return View(db.Employees.OrderBy(o => o.ApplicationUser.Email).ToList());
        //}

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            //return View(db.SuperHeroes.Find(id));

            //Employee employee = db.Employees.Find(id);
            //employee.ApplicationUser.Roles.
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
                //db.SuperHeroes.Add(superHero);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                //db.Roles.Where(r => r.Id == employee.ApplicationUserId .Roles.)
                //employee.RoleName = //employee.ApplicationUser.Roles.u

                //db.Roles.Select(r => r.Id == r.Name == r.Users == r.) <- all are available in Roles 

                //?? Can I get at the AspNetUserRoles table to get the roleID, based on 
                // the employee.ApplicationUserId?
                //  Or can I just save the Role.Name to RoleName upon creation???
                // var whatever = employee.ApplicationUser.GenerateUserIdentityAsync(new ApplicationUserManager


                //employee.RoleName = db.Roles.Where(r => r.Name == "Employee").FirstOrDefault().ToString();
                //employee.RoleName = db.Roles.Where(r => r.Name == "Employee"). .FirstOrDefault().ToString();
                employee.RoleName = "Employee";
                // gets the id of the currently logged in ASPNetUser
                employee.ApplicationUserId = User.Identity.GetUserId();

                db.Employees.Add(employee);
                db.SaveChanges();
                //return RedirectToAction("Details","Employee");
                return RedirectToAction("Details", new { id = employee.ID });
            }
            catch
            {
                //return View(superHero);
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
                // TODO: Add update logic here
                Employee thisEmployee = db.Employees.Find(id);
                thisEmployee.Zip = employee.Zip;
                // thisEmployee.ID

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
            //return View();
            Employee employee = db.Employees.Find(id);
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                // TODO: Add delete logic here
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
