using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        //GET: Customer Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Customer create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET: Customer Delete
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _ctx.Customers.Find(id);
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //POST: Customer Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Customer customer = _ctx.Customers.Find(id);
            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Customer Edit
        public ActionResult Edit(int? id)
        {
            Customer customer = _ctx.Customers.Find(id);
            return View(customer);
        }

        //POST: Customer Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _ctx.Entry(customer).State = EntityState.Modified;
            _ctx.SaveChanges();
            return RedirectToAction("Index")
        }
        //GET: Customer Details
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _ctx.Customers.Find(id);
            return View(customer);
        }
    }
}