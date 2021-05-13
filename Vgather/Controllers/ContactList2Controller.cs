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
    public class ContactList2Controller : Controller
    {
        private EventManagementEntities db = new EventManagementEntities();

        // GET: ContactList2
        public ActionResult Index()
        {
            var contactList2 = db.ContactList2.Include(c => c.invitation2);
            return View(contactList2.ToList());
        }

        // GET: ContactList2/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactList2 contactList2 = db.ContactList2.Find(id);
            if (contactList2 == null)
            {
                return HttpNotFound();
            }
            return View(contactList2);
        }

        // GET: ContactList2/Create
        public ActionResult Create()
        {
            ViewBag.ContactName = new SelectList(db.invitation2, "ContactName", "ContactNumber");
            return View();
        }

        // POST: ContactList2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ContactName,ContactNumber")] ContactList2 contactList2)
        {
            if (ModelState.IsValid)
            {
                db.ContactList2.Add(contactList2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactName = new SelectList(db.invitation2, "ContactName", "ContactNumber", contactList2.ContactName);
            return View(contactList2);
        }

        // GET: ContactList2/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactList2 contactList2 = db.ContactList2.Find(id);
            if (contactList2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactName = new SelectList(db.invitation2, "ContactName", "ContactNumber", contactList2.ContactName);
            return View(contactList2);
        }

        // POST: ContactList2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,ContactName,ContactNumber")] ContactList2 contactList2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactList2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactName = new SelectList(db.invitation2, "ContactName", "ContactNumber", contactList2.ContactName);
            return View(contactList2);
        }

        // GET: ContactList2/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactList2 contactList2 = db.ContactList2.Find(id);
            if (contactList2 == null)
            {
                return HttpNotFound();
            }
            return View(contactList2);
        }

        // POST: ContactList2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ContactList2 contactList2 = db.ContactList2.Find(id);
            db.ContactList2.Remove(contactList2);
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
