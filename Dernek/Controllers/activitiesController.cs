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
using Microsoft.AspNet.Identity.EntityFramework;
using Dernek.Tools;
using Dernek.Models.EnumProperty;

namespace Dernek.Controllers
{
    [Authorize]
    public class activitiesController : Controller
    {
        
        private WorkOfTables workOfTables = new WorkOfTables(new ApplicationDbContext());


        // GET: activities
        public ActionResult Index()
        {

            using (var workOfTables = new WorkOfTables(new ApplicationDbContext()))
            {
                return View(workOfTables.Activity.GetAllReverseByDate());
            }

            //return View(db.activity.ToList());
        }

        // GET: activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //activity activity = db.activity.Find(id);
            activity activity = workOfTables.Activity.Get(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: activities/Create
        public ActionResult Create()
        {
            //var appUsers = (from m in workOfTables.Users select m).ToList();
            var appUsers = workOfTables.Users.ToList();
            ViewBag.users = new MultiSelectList(appUsers, "Id", "UName");
            return View();
        }

        // POST: activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,activityTypes,name,recordDate,startDate,endDate,location,price,quantity,cost,description,km,createrUserId,dancerPerRate,status")] activity activity, ICollection<string> UserId)
        {
            activity.createrUserId = User.Identity.GetUserId();
            foreach (var usr in UserId)
            {
                //var user = db.Users.Where(m => m.Id == usr).FirstOrDefault();
                var user = workOfTables.Users.Where(m => m.Id == usr).FirstOrDefault();
                activity.ApplicationUsers.Add(user);
            }
            if (ModelState.IsValid)
            {
                //db.activity.Add(activity);
                //db.SaveChanges();
                workOfTables.Activity.Add(activity);
                workOfTables.Complete();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationDbContext db = new ApplicationDbContext();
            activity activity = db.activity.Include(x => x.Payments).Where(m => m.id == id).First();
            
            //activity activity = workOfTables.Activity.Get(id);

            //var TTTTT = (from m in workOfTables.Activity.GetAll()
            //             orderby m.id descending
            //             select m).LastOrDefault();

            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }
        //public ActionResult AddExtraPayment(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //activity activity = db.activity.Find(id);
        //    activity activity = workOfTables.Activity.Get(id);
        //    return View();
        //}
        // POST: activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        public ActionResult AddExtraPayment(int id)
        {
            var HanUserId = workOfTables.Users.Where(m => m.UserName == "Han").First().Id;
            return PartialView("AddExtraPayment", 
                new payment {
                    applicationUserId =HanUserId,
                    activityId=id
                });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,type,name,recordDate,startDate,endDate,location,price,quantity,cost,description,km,createrUserId,dancerPerRate,status")] activity activity)
            public ActionResult Edit(activity activity)
        {
             ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                

                db.Entry(activity).State = EntityState.Modified;
                activity oldValues = db.activity.Find(activity.id);
                db.SaveChanges();

                ApplicationUser HanUser = db.Users.Where(x => x.UserName == "Han").FirstOrDefault();
                var createrUserID = User.Identity.GetUserId();

                if (activity.status == Models.EnumProperty.enums.activityStatus.Closed && oldValues.status != activity.status)
                {
                    
                    payment societyPayment = new payment
                    {
                        createrUserId = createrUserID,
                        activityId = activity.id,
                        description = "System - Activity Total Payment",
                        mounth = new DateTime(activity.endDate.Year, 1, 1),
                        paymentDate = DateTime.Now,
                        payTotal = activity.price,
                        applicationUserId = HanUser.Id
                    };

                    db.payment.Add(societyPayment);

                    var activityAndUser = db.activity.Include(x => x.ApplicationUsers).Where(s => s.id == activity.id).First();
                    //double memberPayment = AbstractTools.MemberPayTotalForActivity(activityAndUser);

                    foreach (ApplicationUser member in activityAndUser.ApplicationUsers)
                    {
                        //Üyenin hesabına giriş yapılıyor
                        payment newMemberPayment = new payment
                        {
                            createrUserId = createrUserID,
                            applicationUserId = member.Id,
                            activityId = activity.id,
                            description = "System - Activity User Payment",
                            mounth = new DateTime(activityAndUser.endDate.Year, 1, 1),
                            paymentDate = DateTime.Now,
                            payTotal = activity.price
                        };
                        db.payment.Add(newMemberPayment);


                        //Kasadan üyeye çıkış yapılıyor
                        payment societyPaymentOut = new payment
                        {
                            createrUserId = createrUserID,
                            activityId = activity.id,
                            description = "System - Activity Out Payment",
                            mounth = new DateTime(activity.endDate.Year, 1, 1),
                            paymentDate = DateTime.Now,
                            payTotal = activity.price * -1,
                            applicationUserId = HanUser.Id
                        };
                        db.payment.Add(societyPaymentOut);
                    }

                    db.SaveChanges();
                }

                //Add Extra Payments
                if (activity.Payments.Count > 0)
                {
                    foreach (payment extraPayment in activity.Payments)
                    {
                        extraPayment.createrUserId = createrUserID;
                        db.payment.Add(extraPayment);
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //activity activity = db.activity.Find(id);
            activity activity = workOfTables.Activity.Get(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }
        public ActionResult DeleteExtraPayment(int id,int activityId)
        {
            //return RedirectToAction("Delete", "payments", new { id = id });
            payment payment = workOfTables.Payment.Get(id);
            workOfTables.Payment.Delete(payment);
            workOfTables.Complete();
            return RedirectToAction("Edit", "activities",new {id= activityId });
        }

        // POST: activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //activity activity = db.activity.Find(id);
            //db.activity.Remove(activity);
            //db.SaveChanges();
            activity activity = workOfTables.Activity.Get(id);
            workOfTables.Activity.Delete(activity);
            workOfTables.Complete();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
