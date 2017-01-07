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

namespace Dernek.Controllers
{
    [Authorize]

    public class userDetailsController : Controller
    {
        

        private ApplicationDbContext db = new ApplicationDbContext();
        private WorkOfTables workOfTables = new WorkOfTables(new ApplicationDbContext());
        // GET: userDetails
        public ActionResult Index()
        {
            var activeUserId = User.Identity.GetUserId();
            var user = workOfTables.Users.Include(m => m.userDetail).Where(x => x.Id == activeUserId).FirstOrDefault();
            if (user.userDetail != null)
            {
                ViewBag.userImage = String.Format("data:image/{0};base64,{1}",user.userDetail.userImageType, Convert.ToBase64String(user.userDetail.userImage));
            }
             
            return View(user);
        }

        // GET: userDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetail userDetail = db.userDetail.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: userDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,surname,job,serialNumber,identityNo,placeOfBirth,dateOfBirth,motherName,fatherName,bloodGroup,maritalStatus,county,city,district,town,iban,skinNo,rowNo,familyRowNo,placeOfGiven,dateOfGiven,tel1,tel2,address,facebookAccount,tweeterAccount,instagramAccount,swarmAccount")] userDetail userDetail, HttpPostedFileBase userImage)
        {
            if (ModelState.IsValid)
            {
                if (userImage != null)
                {
                    //    //attach the uploaded image to the object before saving to Database
                    userDetail.userImage = new byte[userImage.ContentLength];
                    userDetail.userImageType = userImage.ContentType.ToString();
                    userImage.InputStream.Read(userDetail.userImage, 0, userImage.ContentLength);
                //    artwork.ImageData = new byte[image.ContentLength];
                //    image.InputStream.Read(artwork.ImageData, 0, image.ContentLength);

                //    //Save image to file
                //    var filename = image.FileName;
                //    var filePathOriginal = Server.MapPath("/Content/Uploads/Originals");
                //    var filePathThumbnail = Server.MapPath("/Content/Uploads/Thumbnails");
                //    string savedFileName = Path.Combine(filePathOriginal, filename);
                //    image.SaveAs(savedFileName);

                //    //Read image back from file and create thumbnail from it
                //    var imageFile = Path.Combine(Server.MapPath("~/Content/Uploads/Originals"), filename);
                //    using (var srcImage = Image.FromFile(imageFile))
                //    using (var newImage = new Bitmap(100, 100))
                //    using (var graphics = Graphics.FromImage(newImage))
                //    using (var stream = new MemoryStream())
                //    {
                //        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //        graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 100));
                //        newImage.Save(stream, ImageFormat.Png);
                //        var thumbNew = File(stream.ToArray(), "image/png");
                //        artwork.ArtworkThumbnail = thumbNew.FileContents;
                //    }
                //}

                ////Save model object to database
                //db.ArtWorks.Add(artwork);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                }

                userDetail.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                db.userDetail.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetail);
        }

        // GET: userDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetail userDetail = db.userDetail.Find(id);
            ViewBag.userImage = String.Format("data:image/{0};base64,{1}",userDetail.userImageType, Convert.ToBase64String(userDetail.userImage));
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: userDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,surname,job,serialNumber,identityNo,placeOfBirth,dateOfBirth,motherName,fatherName,bloodGroup,maritalStatus,county,city,district,town,iban,skinNo,rowNo,familyRowNo,placeOfGiven,dateOfGiven,tel1,tel2,address,facebookAccount,tweeterAccount,instagramAccount,swarmAccount")] userDetail userDetail, HttpPostedFileBase userImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetail).State = EntityState.Modified;

                if (userImage != null)
                {
                    userDetail.userImage = new byte[userImage.ContentLength];
                    userDetail.userImageType = userImage.ContentType.ToString();
                    userImage.InputStream.Read(userDetail.userImage, 0, userImage.ContentLength);
                }
                else {

                    db.Entry(userDetail).Property(m => m.userImage).IsModified = false;
                    db.Entry(userDetail).Property(m => m.userImageType).IsModified = false;
                }
                
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: userDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetail userDetail = db.userDetail.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: userDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userDetail userDetail = db.userDetail.Find(id);
            db.userDetail.Remove(userDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
