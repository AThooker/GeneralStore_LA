using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transList = _ctx.Transactions.ToList();
            List<Transaction> orderList = transList.OrderBy(t => t.TimeCreated).ToList();
            return View(orderList);
        }

        //GET: Transaction Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Transaction Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _ctx.Transactions.Add(transaction);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Transaction Delete
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _ctx.Transactions.Find(id);
            if(transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //POST: Transaction Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Transaction transaction = _ctx.Transactions.Find(id);
            _ctx.Transactions.Remove(transaction);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Edit Transaction
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _ctx.Transactions.Find(id);
            if(transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //POST: Transaction Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _ctx.Transactions.Add(transaction);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Transaction Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _ctx.Transactions.Find(id);
            return View(transaction);
        }
    }
}