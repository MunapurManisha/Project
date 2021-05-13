using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vgather.Models;

namespace Vgather.Controllers
{
    public class AdminFeedbackFormsController : Controller
    {
        private EventManagementEntities db = new EventManagementEntities();

        // GET: AdminFeedbackForms
        public ActionResult Index()
        {
            return View(db.AdminFeedbackForms.ToList());
        }

        // GET: AdminFeedbackForms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminFeedbackForm adminFeedbackForm = db.AdminFeedbackForms.Find(id);
            if (adminFeedbackForm == null)
            {
                return HttpNotFound();
            }
            return View(adminFeedbackForm);
        }

        // GET: AdminFeedbackForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminFeedbackForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ques1,UserId")] AdminFeedbackForm adminFeedbackForm)
        {
            if (ModelState.IsValid)
            {
                db.AdminFeedbackForms.Add(adminFeedbackForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminFeedbackForm);
        }

        // GET: AdminFeedbackForms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminFeedbackForm adminFeedbackForm = db.AdminFeedbackForms.Find(id);
            if (adminFeedbackForm == null)
            {
                return HttpNotFound();
            }
            return View(adminFeedbackForm);
        }

        // POST: AdminFeedbackForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ques1,UserId")] AdminFeedbackForm adminFeedbackForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminFeedbackForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminFeedbackForm);
        }

        // GET: AdminFeedbackForms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminFeedbackForm adminFeedbackForm = db.AdminFeedbackForms.Find(id);
            if (adminFeedbackForm == null)
            {
                return HttpNotFound();
            }
            return View(adminFeedbackForm);
        }

        // POST: AdminFeedbackForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AdminFeedbackForm adminFeedbackForm = db.AdminFeedbackForms.Find(id);
            db.AdminFeedbackForms.Remove(adminFeedbackForm);
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
