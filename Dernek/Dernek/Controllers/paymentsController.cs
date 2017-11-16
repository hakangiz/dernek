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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
            if (db.payment.Count() > 0)
            {
                ViewData["TotalBalance"] = db.payment.Where(m=>m.ApplicationUser.UserName=="Han").Sum(m => m.payTotal);
            }
            else { ViewData["TotalBalance"] = 0.ToString("C0"); }
            List<payment> paymentList = db.payment.Include(p => p.Activity).Include(x => x.ApplicationUser).OrderByDescending(c => c.id).ToList();
            return View(paymentList);
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
            ViewBag.users = new MultiSelectList(appUsers, "Id", "UName");

            var paymentModel = new payment();
            paymentModel.createrUserId = User.Identity.GetUserId();

            return View(paymentModel);
        }

        // POST: payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,payTotal,paymentDate,createrUserId,mounth,description")] payment payment, IList<string> UserId, string activity)
        {

            if (ModelState.IsValid)
            {
                MonthName monthName = (MonthName)payment.mounth.Date.Month;
                var appU = new ApplicationUser();
                foreach (var user in UserId)
                {
                    appU = workOfTables.Users.Where(x => x.Id == user).First();
                    var pay = new payment {
                        ApplicationUser=appU,
                        createrUserId= User.Identity.GetUserId(),
                        Activity= workOfTables.Activity.Get(Convert.ToInt32(activity)),
                        description=payment.description,
                        mounth=payment.mounth,
                        paymentDate=payment.paymentDate,
                        payTotal=payment.payTotal,
                };
                    if (appU.UserName != "Han" && payment.payTotal>0)
                    {
                        var UHan = new ApplicationUser();
                        UHan = workOfTables.Users.Where(x => x.UserName == "Han").First();
                        var HanPay = new payment
                        {
                            ApplicationUser = UHan,
                            createrUserId = User.Identity.GetUserId(),
                            Activity = workOfTables.Activity.Get(Convert.ToInt32(activity)),
                            description = string.Format("{0} kullanıcısının {1} tarihli ödemesidir.",appU.UserName, monthName),
                            mounth = payment.mounth,
                            paymentDate = payment.paymentDate,
                            payTotal = payment.payTotal,
                        };
                        workOfTables.Payment.Add(HanPay);
                    }
                    workOfTables.Payment.Add(pay);
                }
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
            payment otherpay=workOfTables.Payment.Get(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            var actList = workOfTables.Activity.GetAll();
            ViewBag.activities = new MultiSelectList(actList, "Id", "name");
            return View(payment);
        }

        // POST: payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,activityId,payTotal,paymentDate,createrUserId,mounth,description,applicationUserId,createdUserId")] payment payment,string activity)
        {
            if (ModelState.IsValid)
            {
                payment.activityId = Convert.ToInt32(activity);

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
            var payments = workOfTables.Payment.GetUserPaymentsByUserName(userName);
            return View("Index", payments);
        }

        public ActionResult GetPaymentsByUserId(string userId)
        {

            if (userId == null)
            {
                //return View();
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                userId = user.Id;
            }
            var payments = workOfTables.Payment.GetUserPaymentsByUserId(userId);
            ViewData["TotalBalance"] = payments.Sum(m => m.payTotal);

            ViewData["users"] = new MultiSelectList(workOfTables.Users, "Id", "UName");
            return View("UserReport", payments.Reverse());
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

    public enum MonthName
    {
        IlkAyBos,
        Ocak,
        Şubat,
        Mart,
        Nisan,
        Mayıs,
        Haziran,
        Temmuz,
        Ağustos,
        Eylül,
        Ekim,
        Kasım,
        Aralık
    }
}
