using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionBangla.Models;

namespace FashionBangla.Controllers
{
    public class ProductsController : Controller
    {
        private FashionDbContext db = new FashionDbContext();
        
        // GET: /Products/
        public ActionResult Index(int? Id, int page=1)
        {
            int size = 3; 
            ViewBag.CurrentPage = page;
            ViewBag.id = Id;
            if (Id > 0)
            {
             
                ViewBag.TotalPages = (int)Math.Ceiling((double)db.Products.Where(c => c.CategoryId == Id).Count()/size);
                var products = db.Products.Where(c => c.CategoryId == Id).Include(p => p.Category).OrderBy(c=>c.ProductId).Skip((page-1)*size).Take(size);
                return View(products.ToList());
            }
            else
            {

                ViewBag.TotalPages = (int)Math.Ceiling((double)db.Products.Count() / size);
                var products = db.Products.Include(p => p.Category).OrderBy(c => c.ProductId).Skip((page - 1) * size).Take(size);
                return View(products.ToList());
            } 
            
        }
        
        // GET: /Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
