using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var orderedList = transactions.OrderByDescending(t => t.Product.Price).ToList();
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
    }
}