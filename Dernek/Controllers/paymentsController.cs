using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dernek.Models;
using Dernek.Repository;

namespace Dernek.Controllers
{
    [Authorize]
    public class paymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private WorkOfTables workOfTables = new WorkOfTables(new ApplicationDbContext());

        // GET: payments
        public ActionResult Index()
        {
            return View(db.payment.Include(p=>p.Activity).Include(x=>x.ApplicationUser).ToList());
        }

        // GET: payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: payments/Create
        public ActionResult Create()
        {
            //var appUsers = (from m in workOfTables.Users select m).ToList();
            var actList = workOfTables.Activity.GetAll();
            ViewBag.activities = new MultiSelectList(actList, "Id", "name");
            var appUsers = workOfTables.Users.ToList();
            ViewBag.users = new MultiSelectList(appUsers, "Id", "UserName");

            return View();
        }

        // POST: payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,payTotal,paymentDate,createrUserId,mounth,description")] payment payment,string UserId,string activity)
        {
            if (ModelState.IsValid)
            {
                var appU = new ApplicationUser();
                appU = workOfTables.Users.Where(x=>x.Id==UserId).First();
                payment.ApplicationUser = appU;

                var act = new activity();
                act = workOfTables.Activity.Get(Convert.ToInt32(activity));
                payment.Activity = act;



                //db.payment.Add(payment);
                //db.SaveChanges();
                workOfTables.Payment.Add(payment);
                workOfTables.Complete();
                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,activityId,payTotal,paymentDate,createrUserId,mounth,description")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //payment payment = db.payment.Include(x => x.Activity).Where(m => m.id == id).FirstOrDefault();
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payment payment = db.payment.Find(id);
            db.payment.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetPaymentsByUserName(string userName)
        {
            var payments= workOfTables.Payment.GetUserPaymentsByUserName(userName);
            return View(payments);
        }

        public ActionResult GetPaymentsByUserId(string userId)
        {
            var payments = workOfTables.Payment.GetUserPaymentsByUserId(userId);
            return View("Index", payments);
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
