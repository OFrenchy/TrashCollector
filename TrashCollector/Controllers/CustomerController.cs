using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext db;

        public CustomerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Customer
        //public ActionResult Index()
        //{
        //    //return View(db.SuperHeroes.OrderBy(o => o.Name).ToList());
        //    return View(db.Customers.OrderBy(o => o.ApplicationUser.Email).ToList());
        //}

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            //return View(db.SuperHeroes.Find(id));
            return View(db.Customers.Find(id));
        }
        public ActionResult PayBill(int id)
        {
            var customer = db.Customers.Find(id));

            // insert actual payment transaction here
            bool paymentSuccessful = true; // would actually be an api call
            
            if (paymentSuccessful)
            {
                customer.Bill = 0;
                customer.BillDetails = null;
                db.SaveChanges();
            }

            return View(db.Customers.Find(id));
        }

            // GET: Customer/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                //db.SuperHeroes.Add(superHero);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                //customer.Name = 


                //customer.ApplicationUser = "Customer";
                //customer.BillDetails = "";
                customer.RoleName = "Customer";
                customer.ApplicationUserId = User.Identity.GetUserId();
                //customer.ApplicationUser = User.Identity.GetUserName();


                db.Customers.Add(customer);
                db.SaveChanges();
                //return RedirectToAction("Edit");
                return RedirectToAction("Edit", new { id = customer.ID });
            }
            catch
            {
                //return View(superHero);
                return View(customer);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            List<SelectListItem> daysOfWeek = new List<SelectListItem>();
            DayOfWeek dow = DayOfWeek.Monday;
            for (int i = 1; i < 6; i++)
            {
                daysOfWeek.Add((new SelectListItem() { Text = dow.ToString(), Value = i.ToString() }));
                dow++;
            }
            ViewBag.DaysOfWeek = daysOfWeek;

            return View(db.Customers.Find(id));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                Customer thisCustomer = db.Customers.Find(id);

                thisCustomer.Name = customer.Name;
                thisCustomer.Street = customer.Street;
                thisCustomer.City = customer.City;
                thisCustomer.State = customer.State;
                thisCustomer.Zip = customer.Zip;
                thisCustomer.DayOfWeekPickup = customer.DayOfWeekPickup;
                thisCustomer.StopDate = customer.StopDate;
                thisCustomer.StartDate = customer.StartDate;
                thisCustomer.SpecialPickupDate = customer.SpecialPickupDate;

                db.SaveChanges();
                //return View(customer);
                //return RedirectToAction("Details");
                return RedirectToAction("Details", new { id = customer.ID });
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            //return View();
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                // TODO: Add delete logic here
                db.Customers.Remove(db.Customers.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(customer);
            }
        }
    }





}
