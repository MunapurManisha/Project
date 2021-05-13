using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vgather.Models;

namespace Vgather.Controllers
{
    public class HomepageController : Controller
    {
        EventManagementEntities db = new EventManagementEntities();
        // GET: Homepage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        #region service1
        public ActionResult Service1()
        {
            ViewBag.Message = "services you offer will be shown here";
            return View();
           // return RedirectToAction("Create", "Registration");
        }
        #endregion
        public ActionResult Articles()
        {
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Participants()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Participants(Participant obj)
        {

            if (ModelState.IsValid)
            {
                
                db.Participants.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index", "Homepage");

            }
            return View(obj);
           
        }
        [HttpGet]
        public ActionResult Addimage()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Addimage(Image img, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                filename = filename + extension;
                img.location = "~/Models/img/" + filename;
                filename = Path.Combine(Server.MapPath("~/Models/img/"), filename);
                image.SaveAs(filename);
                db.Images.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);

        }
       




    }
}