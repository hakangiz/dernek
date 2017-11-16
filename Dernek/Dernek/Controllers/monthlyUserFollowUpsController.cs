using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dernek.Models;
using Microsoft.AspNet.Identity;
using Dernek.Repository;
using Microsoft.AspNet.Identity.Owin;

namespace Dernek.Controllers
{
    [Authorize]
    public class monthlyUserFollowUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private WorkOfTables workOfTables = new WorkOfTables(new ApplicationDbContext());

        // GET: monthlyUserFollowUps
        public ActionResult Index()
        {
            var list = workOfTables.MonthlyUser.GetAllReverseByDate();
            return View(list);
        }

        // GET: monthlyUserFollowUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthlyUserFollowUp monthlyUserFollowUp = db.monthlyUserFollowUps.Find(id);
            if (monthlyUserFollowUp == null)
            {
                return HttpNotFound();
            }
            return View(monthlyUserFollowUp);
        }

        // GET: monthlyUserFollowUps/Create
        public ActionResult Create()
        {
            var appUsers = workOfTables.Users.ToList();
            ViewBag.users = new MultiSelectList(appUsers, "Id", "UName");
            return View();
        }

        // POST: monthlyUserFollowUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,month,followStatus,followDate,actionDate,createrUserId")] monthlyUserFollowUp monthlyUserFollowUp, ICollection<string> UserId)
        {
            if (ModelState.IsValid)
            {
                foreach (var usr in db.Users.ToList())
                {
                    monthlyUserFollowUp follow = new monthlyUserFollowUp {
                        actionDate= monthlyUserFollowUp.actionDate,
                        createrUserId= User.Identity.GetUserId(),
                        followDate= monthlyUserFollowUp.followDate,
                        followStatus = true,
                        month = monthlyUserFollowUp.month
                    };
                    //monthlyUserFollowUp.createrUserId = User.Identity.GetUserId();
                    follow.ApplicationUser = usr;
                    if (UserId.Contains(usr.Id)==false)
                    {
                        follow.followStatus = false;
                    }
                    db.monthlyUserFollowUps.Add(follow);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        // GET: monthlyUserFollowUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthlyUserFollowUp monthlyUserFollowUp = db.monthlyUserFollowUps.Find(id);
            if (monthlyUserFollowUp == null)
            {
                return HttpNotFound();
            }
            return View(monthlyUserFollowUp);
        }

        // POST: monthlyUserFollowUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,month,followStatus,followDate,actionDate,createrUserId")] monthlyUserFollowUp monthlyUserFollowUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyUserFollowUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monthlyUserFollowUp);
        }

        // GET: monthlyUserFollowUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthlyUserFollowUp monthlyUserFollowUp = db.monthlyUserFollowUps.Find(id);
            if (monthlyUserFollowUp == null)
            {
                return HttpNotFound();
            }
            return View(monthlyUserFollowUp);
        }

        // POST: monthlyUserFollowUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            monthlyUserFollowUp monthlyUserFollowUp = db.monthlyUserFollowUps.Find(id);
            db.monthlyUserFollowUps.Remove(monthlyUserFollowUp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetMonthlyFollowByUserId(string userId)
        {

            if (userId == null)
            {
                //return View();
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                userId = user.Id;
            }
            var follow = workOfTables.MonthlyUser.GetUserMounthlyFollowByUserId(userId);

            ViewData["users"] = new MultiSelectList(workOfTables.Users, "Id", "UName");
            return View("UserReport", follow.Reverse());
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
