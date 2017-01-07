using Dernek.Models;
using Dernek.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dernek.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        WorkOfTables workOftables = new WorkOfTables(new ApplicationDbContext());
        //ApplicationDbContext db = new ApplicationDbContext();
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Manager")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // GET: Users
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var user = User.Identity;
                //ViewBag.Name = user.Name;
                ////	ApplicationDbContext context = new ApplicationDbContext();
                ////	var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                ////var s=	UserManager.GetRoles(user.GetUserId());
                //ViewBag.displayMenu = "No";

                //if (isAdminUser())
                //{
                //    ViewBag.displayMenu = "Yes";
                //}
                //return View();
                var users = workOftables.UserDetail.GetAll();
                return View(users);
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }


            return View();
        }
        
    }
}