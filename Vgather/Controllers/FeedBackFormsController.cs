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
    public class FeedBackFormsController : Controller
    {
        private EventManagementEntities db = new EventManagementEntities();

        // GET: FeedBackForms
        public ActionResult Index()
        {
            var feedBackForms = db.FeedBackForms.Include(f => f.AdminFeedbackForm);
            return View(feedBackForms.ToList());
        }

        // GET: FeedBackForms/Details/5
        // GET: FeedBackForms/Create
        public ActionResult Create()
        {
            ViewBag.Ques1 = new SelectList(db.AdminFeedbackForms, "Ques1", "Ques1");
            return View();
        }

        // POST: FeedBackForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Ques1,Rating,Description")] FeedBackForm feedBackForm)
        {
            if (ModelState.IsValid)
            {
                db.FeedBackForms.Add(feedBackForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ques1 = new SelectList(db.AdminFeedbackForms, "Ques1", "Ques1", feedBackForm.Ques1);
            return View(feedBackForm);
        }

        // GET: FeedBackForms/Edit/5
      
        // GET: FeedBackForms/Delete/5
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult TriggerFeed()
        {
            return View();
        }
        // POST: Registrations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TriggerFeed([Bind(Include = "UserId,ques1,rating")] TriggerFeed trigger)
        {

            if (ModelState.IsValid)
            {
                db.TriggerFeeds.Add(trigger);
                db.SaveChanges();
                return RedirectToAction("Service1","Homepage");
            }
            return View();
        }

    }
}
