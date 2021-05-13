using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Web.Mvc;
using Vgather.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;


namespace Vgather.Controllers
{
    public class AdminController : Controller
    {
        private EventManagementEntities db = new EventManagementEntities();
        // GET: Registration
        public ActionResult Index()
        {
            var even = db.prc_allEvents().ToList();
            ViewBag.userdetails = even;
            return View();
        }
        // GET: Admin
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Select()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(Admin objUser, FormCollection frm)
        {
            if (ModelState.IsValid)
            {

                var obj = db.Admins.Where(a => a.Email_ID.Equals(objUser.Email_ID) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    //Session["Email"] = obj.Email_ID;
                    Session["Email"] = obj.Email_ID.ToString();
                    Session["password"] = obj.Password.ToString();


                    //return RedirectToAction("UserDashBoard");

                    return Json("Registration successful ", JsonRequestBehavior.AllowGet);
                }

            }
            return Json(objUser);
        }
        /// This Actionmethod is used to validate emailid and password while logging in
        /// </summary>
        /// <param name="model"></param>
        /// <param name="frm"></param>
        /// <returns>It will be redirected to services page</returns>
        #region LoginValidationEmail
        public ActionResult CheckValidUser(Admin model, FormCollection frm)
        {
            string result = "Fail";
            try
            {
                model.Email_ID = frm[0];
                model.Password = frm[1];

                var DataItem = db.Admins.Where(x => x.Email_ID == model.Email_ID && x.Password == model.Password).SingleOrDefault();
                if (DataItem != null)
                {
                    Session["email"] = DataItem.Email_ID.ToString();
                    result = "Success";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                ViewBag.Message = "Email id already exists...pls re-enter";
                result = "Fail";
                return View();
            }
        }
        #endregion
        public ActionResult AfterLogin()
        {
            if (Session["email"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Service1", "Admin");
            }
        }
        public ActionResult Service1()
        {
            ViewBag.Message = "services you offer will be shown here";
            return View();
            // return RedirectToAction("Create", "Registration");
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


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EventIndex()
        {
            return View(db.EventBookings.ToList());
        }




    }
}
