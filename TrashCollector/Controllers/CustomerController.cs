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

        // GET: Customer
        public ActionResult Index()
        {
            //return View(db.SuperHeroes.OrderBy(o => o.Name).ToList());
            return View(db.Customers.OrderBy(o => o.ApplicationUser.Email).ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            //return View(db.SuperHeroes.Find(id));
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
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            return View(db.Customers.Find(id));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                Customer thisCustomer = db.Customers.Find(id);
                thisCustomer.Zip = customer.Zip;
                // thisCustomer.ID

                db.SaveChanges();
                return RedirectToAction("Index");
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
