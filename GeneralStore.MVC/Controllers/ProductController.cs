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
    public class ProductController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            List<Product> productList = _ctx.Products.ToList();
            List<Product> orderList = productList.OrderByDescending(p => p.Price).ToList();
            return View(orderList);
        }

        //GET: Product Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Product Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Product Delete
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _ctx.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //POST: Product Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _ctx.Products.Find(id);
            _ctx.Products.Remove(product);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Product Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _ctx.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        //POST: Product Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            _ctx.Entry(product).State = EntityState.Modified;
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET: Product Details
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _ctx.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}