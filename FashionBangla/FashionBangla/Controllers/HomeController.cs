using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using FashionBangla.Models;

namespace FashionBangla.Controllers
{
    public class HomeController : Controller
    {
        //ApplicationDbContext ctx = new ApplicationDbContext();
        //ApplicationUser user;
        //Customer customer;
        FashionDbContext db = new FashionDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        public ActionResult Event()
        {


            return View();
        }
        [Authorize]
        public ActionResult Cart(int? id)
        {
            string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = ctx.Users.FirstOrDefault(x => x.Id == currentUserId);
            Customer c= db.Customers.First(x => x.UserId.Trim() == currentUserId.Trim());
            if (id.HasValue)
            {
                if(db.CartItems.Any(ci=> ci.CustomerId==c.CustomerId && ci.ProductId== id))
                {
                    var item =db.CartItems.First(ci => ci.CustomerId == c.CustomerId && ci.ProductId == id);
                    item.Quantity++;
                }
                else
                {
                    db.CartItems.Add(new CartItem { CustomerId = c.CustomerId, ProductId = id.Value, Quantity=1 });
                }
                db.SaveChanges();
            }
            var data = new List<CartItemVM>();
            var items = db.CartItems.Where(x => x.CustomerId == c.CustomerId).ToList();
            foreach (var item in items)
            {
                var v = new CartItemVM { CartItemId = item.CartItemId, ProductId = item.ProductId, Quantity = item.Quantity, CustomerId=c.CustomerId };
                var p= db.Products.First(x => x.ProductId == item.ProductId);
                v.ProductName = p.ProductName;
                v.UnitPrice = (decimal)p.UnitPrice;
                v.Amount = item.Quantity * (decimal)p.UnitPrice;
                data.Add(v);
            }
            ViewBag.Total = data.Sum(x => x.Amount);
            return View(data);
        }
        //HTTP GET Home/CheckOut
        public ActionResult CheckOut()
        {
            string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = ctx.Users.FirstOrDefault(x => x.Id == currentUserId);
            Customer c = db.Customers.First(x => x.UserId.Trim() == currentUserId.Trim());
            var complete = false;
            if (!string.IsNullOrEmpty(c.CustomerName) && !string.IsNullOrEmpty(c.Address) && !string.IsNullOrEmpty(c.CCNumber) && !c.CCExpire.HasValue)
                complete = true;
            else
                complete = false;
            ViewBag.Complete = complete;
            return View(new CustomerVM { Id = c.CustomerId, CustomerName = c.CustomerName, Address = c.Address, CCNumber = c.CCNumber, CCExpire = c.CCExpire });
        }
        ////Cart Editable AJax actions
        public JsonResult UpdateQty(int pk, int value)
        {
            var cartItem = db.CartItems.First(x => x.CartItemId == pk);
            cartItem.Quantity = value;
            db.SaveChanges();
            string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = ctx.Users.FirstOrDefault(x => x.Id == currentUserId);
            Customer c = db.Customers.First(x => x.UserId.Trim() == currentUserId.Trim());
            var items = db.CartItems.Where(x => x.CustomerId == c.CustomerId).ToList();
            var total = 0.0M;
            var subtotal = 0.0M;
            foreach (var item in items)
            {
               
                var p = db.Products.First(x => x.ProductId == item.ProductId);
               
                total += item.Quantity * (decimal)p.UnitPrice;
                if (item.CartItemId == item.CartItemId)
                {
                    subtotal = item.Quantity * (decimal)p.UnitPrice;
                }
               
            }
            return Json(new { success = true, msg = new {pk=pk, subtotal = subtotal, total = total } }, JsonRequestBehavior.AllowGet);

        }
        ////////////////////////////////////
    }
}