using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Vgather.Models;

namespace Vgather.Controllers
{
    public class RegistrationController : Controller
    {

        private EventManagementEntities db = new EventManagementEntities();
        // GET: Registration
        public ActionResult Index()
        {
            return View(db.Registrations.ToList());
        }

        /// <summary>
        /// This Actionmethod is used to login by checking the existence of the email id and password in the database
        /// </summary>
        /// <returns>On successful validation it will redirect to welcome page</returns>

        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(Registration objUser, FormCollection frm)
        {
            if (ModelState.IsValid)
            {

                var obj = db.Registrations.Where(a => a.UserId.Equals(objUser.UserId) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    //Session["Email"] = obj.UserId;
                    Session["UserId"] = obj.UserId.ToString();
                    Session["password"] = obj.Password.ToString();


                    //return RedirectToAction("UserDashBoard");

                    return Json("Registration successful ", JsonRequestBehavior.AllowGet);
                }

            }
            return Json(objUser);
        }
        #endregion


        /// <summary>
        /// This Actionmethod is used to allow user to register to our website
        /// </summary>
        /// <returns>Upon registration it will allow user to login</returns>


        #region Register

        public ActionResult Create()
        {
            return View();
        }

        // POST: Registrations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Registration_ID,FirstName,LastName,DoB,Gender,ContactNumber,UserId,Password")] Registration registration)
        {

            if (ModelState.IsValid)
            {
                registration.Password = Security.HashSHA1(registration.Password);
                db.Registrations.Add(registration);
                db.SaveChanges();
                ViewBag.Message = "Registration done successfully";
                return RedirectToAction("Create", "Registration ");
            }
            return View(registration);
        }
        /*public ActionResult Details(string id)
        {
            //var res = Session["id"];
            // var Registration_ID = Session["Email"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }*/





        /// <summary>
        /// This Actionmethod allows to save the details entered in registration form into the database
        /// </summary>
        /// <param name="modal"></param>
        /// <param name="frm"></param>
        /// <returns>mail will be sent to the registered mailid</returns>



        public JsonResult SaveData(Registration modal, FormCollection frm)
        {

            modal.LastName = frm[0];

            db.Registrations.Add(modal);

            db.SaveChanges();
            //BuildEmailTemplate(modal.Registration_ID);


            return Json("Registration successful ", JsonRequestBehavior.AllowGet);

        }

        #endregion

        /// <summary>
        /// These Actionmethods are used to build email template and send mail to registered mailid
        /// </summary>
        /// <param name="regID"></param>

        #region EmailCode
     /*  private void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = db.Registrations.Where(x => x.Registration_ID == regID).FirstOrDefault();
            var url = "http://localhost:2223/" + "Registration/Confirm?regId= " + regID;
            body = body.Replace("ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("VGATHER Registration Successfull", body, regInfo.UserId);
        }

        public static void BuildEmailTemplate(string subjecttext, string bodytext, string sendto)
        {
            string from, to, bcc, cc, subject, body;
            from = "eventdmn@gmail.com";
            to = sendto.Trim();
            bcc = "";
            cc = "";
            subject = subjecttext;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodytext);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.Bcc.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("eventdmn@gmail.com", "newuser123#");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       */
        #endregion


        /// <summary>
        /// This Actionmethod is used to validate emailid and password while logging in
        /// </summary>
        /// <param name="model"></param>
        /// <param name="frm"></param>
        /// <returns>It will be redirected to services page</returns>
        #region LoginValidationEmail
        public ActionResult CheckValidUser(Registration model, FormCollection frm)
        {
            string result = "Fail";
            try            {
                model.UserId = frm[0];
                model.Password = frm[1];

                var DataItem = db.Registrations.Where(x => x.UserId == model.UserId && x.Password == model.Password).SingleOrDefault();
                if (DataItem != null)
                {
                    Session["email"] = DataItem.UserId.ToString();
                    Session["id"] = DataItem.Registration_ID;
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



        /// <summary>
        ///     This Actionmethod checks for valid log in
        /// </summary>
        /// <returns>On successful logging in services page will load</returns>



        public ActionResult AfterLogin()
        {
            if (Session["email"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Service1", "Homepage");
            }
        }


        #endregion


        /// <summary>
        /// This Actionmethod allows to logout
        /// </summary>
        /// <returns>Redirected to home page</returns>

        

        #region  Edit and Delete
        // GET: Registrations/Details/5
        [HttpGet]
        public ActionResult Details()
         {
            int userId = Convert.ToInt32(Session["id"]);
            if (userId == 0)
                {
                return RedirectToAction("Service1", "Homepage");
            }
            return View(db.Registrations.Find(userId));
         }

      
       

        //// GET: Registrations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Registration_ID,FirstName,LastName,DoB,Gender,ContactNumber,UserId,Password,question1,question2,question3")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registration);
        }

        //// GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Create", "FeedBackForms");
        }

        #endregion
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
         [HttpGet]
         public ActionResult ForgotPassword()
         {
             return View();
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ForgotPassword(ForgotPassword ob)
         {
             string message = "";
             if (ModelState.IsValid)
             {

                 var data = db.Registrations.FirstOrDefault(x => x.UserId == ob.UserId);
                 if (data != null)
                 {
                     if (string.Compare(ob.Ques1, data.question1) == 0 && string.Compare(ob.Ques2, data.question2) == 0 && string.Compare(ob.Ques3, data.question3) == 0)
                     {
                        return RedirectToAction("ResetPassword");//, new { UserId = data.UserId });
                     }
                     else
                     {
                         message = "Wrong Answers to the Questions";
                     }
                 }
                 else
                 {
                     message = "User ID does not Exist";
                 }
             }
             ViewBag.Message = message;
             return View(ob);
         }
         [HttpGet]
         public ActionResult ResetPassword(string UserId)
         {
             return View(UserId);
         }

         [HttpPost]
         public ActionResult ResetPassword(string UserId, ResetPassword ob)
         {
             string message = "";
             if (ModelState.IsValid)
             {
                // var data = db.Registrations.Find(UserId);
                 //data.Password = ob.Password;
                // db.Entry(data).State = EntityState.Modified;
                 db.SaveChanges();
                 message = "Password Reset Sucessfull !!!";
             }
            ViewBag.Message = String.Format("Reset Successfull");
            return  View();
             //return View(ob);
         }
        //Forgot Password
        public ActionResult FeedbackPopUp()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
             

    }
}
