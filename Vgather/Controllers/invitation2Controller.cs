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
    public class invitation2Controller : Controller
    {
        private EventManagementEntities db = new EventManagementEntities();

        // GET: invitation2
        public ActionResult Index()
        {
            var invitation2 = db.invitation2.Include(i => i.ContactList2);
            return View(invitation2.ToList());
        }

        // GET: invitation2/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invitation2 invitation2 = db.invitation2.Find(id);
            if (invitation2 == null)
            {
                return HttpNotFound();
            }
            return View(invitation2);
        }

        // GET: invitation2/Create
        public ActionResult Create()
        {
            ViewBag.ContactName = new SelectList(db.ContactList2, "ContactName", "ContactName");
            return View();
        }

        // POST: invitation2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactNumber,ContactName,Venue_Name")] invitation2 invitation2)
        {
            if (ModelState.IsValid)
            {
                db.invitation2.Add(invitation2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactName = new SelectList(db.ContactList2, "ContactName", "ContactName", invitation2.ContactName);
            return View(invitation2);
        }

        // GET: invitation2/Edit/5
       /* public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invitation2 invitation2 = db.invitation2.Find(id);
            if (invitation2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactName = new SelectList(db.ContactList2, "ContactName", "UserId", invitation2.ContactName);
            return View(invitation2);
        }

        // POST: invitation2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactNumber,ContactName,Venue_Name")] invitation2 invitation2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitation2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactName = new SelectList(db.ContactList2, "ContactName", "UserId", invitation2.ContactName);
            return View(invitation2);
        }

        // GET: invitation2/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invitation2 invitation2 = db.invitation2.Find(id);
            if (invitation2 == null)
            {
                return HttpNotFound();
            }
            return View(invitation2);
        }

        // POST: invitation2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            invitation2 invitation2 = db.invitation2.Find(id);
            db.invitation2.Remove(invitation2);
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
        }*/
    }
}
