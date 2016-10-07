using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingApp.Models;
using Microsoft.AspNet.Identity;

namespace ShoppingApp.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        [Authorize (Roles="Admin")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create()
        {
           //  ViewBag.CustomerId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                order.CustomerId = user.Id;
                
                var shoppingCarts = db.ShoppingCarts.Where(s => s.CustomerId == user.Id).ToList();
                var shopCount = 0;
                decimal shopTotal = 0.00M;

                foreach (var cart in shoppingCarts)
                {
                    shopCount += cart.Count;
                    var cartItem = db.Items.FirstOrDefault(t => t.Id == cart.ItemId);
                    shopTotal += cartItem.Price * cart.Count;
                }

                order.Total = shopTotal;
                order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Details", "Orders", new {id = order.Id });
            }

            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Orders/Confirm
        public ActionResult Confirm(int id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var shoppingCarts = db.ShoppingCarts.Where(s => s.CustomerId == user.Id).ToList();
            
            

            if (shoppingCarts != null)
            {
                foreach (var cart in shoppingCarts)
                {
                    var orderDetail = new OrderDetail();
                    
                    orderDetail.ItemId = cart.ItemId;
                    orderDetail.Quantity = cart.Count;
                    orderDetail.UnitPrice = cart.Item.Price;
                    orderDetail.OrderId = id;
                    

                    db.OrderDetails.Add(orderDetail);
                    db.ShoppingCarts.Remove(cart);
                }
                db.SaveChanges();
            }

            return View();
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
