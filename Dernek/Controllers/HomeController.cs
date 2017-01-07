using Dernek.Models;
using Dernek.Repository;
using Dernek.Repository.ClassRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Dernek.Controllers
{
    [EnableCors(origins: "http://localhost:54567/api/values", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            //using (var workOfTables = new WorkOfTables(new ApplicationDbContext()))
            //{
            //    workOfTables.Activity.GetAll();
            //}
            
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}