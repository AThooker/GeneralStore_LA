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
    public class TransactionController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactions = _ctx.Transactions.ToList();
            var orderedList = transactions.OrderByDescending(t => t.TimeCreated).ToList();
            return View(orderedList);
        }
        
        // GET: Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // POST: Create
        public ActionResult Create(Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                return View(transaction);
            }

            _ctx.Transactions.Add(transaction);
            var product = _ctx.Products.Find(transaction.ProductId);
            product.InventoryCount -= transaction.Amount;
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET: Product Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _ctx.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //POST: Product Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Transaction transaction = _ctx.Transactions.Find(id);
            Product product = _ctx.Products.Find(transaction.ProductId);
            product.InventoryCount += transaction.Amount;
            _ctx.Transactions.Remove(transaction);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                return View(transaction);
            }

            //grab the transaction before being edited
            Transaction transactionBeforeEdit = _ctx.Transactions.Find(transaction.TransactionId);
            //grab the product the transaction is associated with
            Product product = _ctx.Products.Find(transaction.ProductId);

            //_ctx.Entry(transaction).State = EntityState.Modified;

            //if the new amount is less than it was, add the difference back to the product inventory
            if(transaction.Amount < transactionBeforeEdit.Amount)
            {
                product.InventoryCount += (transactionBeforeEdit.Amount - transaction.Amount);
            }
            //if the new amount is more than it was, take away the difference from the product inventory
            if(transaction.Amount > transactionBeforeEdit.Amount)
            {
                product.InventoryCount -= (transaction.Amount - transactionBeforeEdit.Amount);
            }
            transactionBeforeEdit.Amount = transaction.Amount;
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
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
    }
}