using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;

using Vgather.localhost;
using System.Web.Mvc;
using Vgather.Models;

namespace Vgather.Controllers
{
    public class EventBookingsController : Controller
    {
        private EventManagementEntities db = new EventManagementEntities();

        // GET: EventBookings
        public ActionResult Index()
        {
            var eventBookings = db.EventBookings.Include(e => e.payment_Booking).Include(e => e.EventType1).Include(e => e.Payment).Include(e => e.service_company).Include(e => e.serviceType).Include(e => e.Venue1);
            return View(eventBookings.ToList());
        }

        // GET: EventBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventBooking = db.EventBookings.Find(id);
            if (eventBooking == null)
            {
                return HttpNotFound();
            }
            return View(eventBooking);
        }

        // GET: EventBookings/Create
        public ActionResult Create()
        {
            ViewBag.Pay_Event_BookingID = new SelectList(db.payment_Booking, "Pay_Event_BookingID", "Payment_Type");
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventType1");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "Payment_Status");
            ViewBag.Service_Company_ID = new SelectList(db.service_company, "Service_Company_ID", "Service_Company1");
            ViewBag.servicetypeid = new SelectList(db.serviceTypes, "ServiceTypeID", "ServiceType1");
            ViewBag.Venue_ID = new SelectList(db.Venues, "Venue_ID", "Venue_Name");


            List<SelectListItem> EventName = new List<SelectListItem>();
            EventBooking Drop = new EventBooking();

            List<EventType> Event = db.EventTypes.ToList();
            Event.ForEach(x =>
            {
                EventName.Add(new SelectListItem { Text = x.EventType1, Value = x.EventTypeID.ToString() });

            });
            Drop.EventType = Convert.ToString(EventName);

            return View(Drop);
            // return View();
        }


        [HttpPost]
        public ActionResult GetVenue(string EventTypeID)
        {
            string EventID;
            List<SelectListItem> VenueName = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(EventTypeID))
            {

                EventID = Convert.ToString(EventTypeID);
                List<Venue> Venues = db.Venues.Where(x => x.EventTypeID == EventID).ToList();
                Venues.ForEach(x =>
                {
                    VenueName.Add(new SelectListItem { Text = x.Venue_Name, Value = x.Venue_ID.ToString() });
                });
                Session["VenueName"] = VenueName.ToString();

            }
            return Json(VenueName, JsonRequestBehavior.AllowGet);
        }



        // POST: EventBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_ID,Event_Name,Start_Date,End_Date,No_Of_Participants,Pay_Event_BookingID,Venue_ID,servicetypeid,PaymentID,EventTypeID,Service_Company_ID,Descriptions,EventType,Venue,FoodCompany,ElectricalCompany")] EventBooking eventBooking)
        {
            if (ModelState.IsValid)
            {
                db.EventBookings.Add(eventBooking);
                db.SaveChanges();
                return RedirectToAction("Summary");
            }

            Session["Event_Name"] = eventBooking.Event_Name;
            Session["Start_Date"] = eventBooking.Start_Date;
            Session["End_Date"] = eventBooking.End_Date;
            Session["No_Of_Participants"] = eventBooking.No_Of_Participants;
            Session["EventTypeID"] = eventBooking.EventTypeID;
            Session["Venue_ID"] = eventBooking.Venue_ID;
            Session["FoodCompany"] = eventBooking.FoodCompany;
            Session["ElectricalCompany"] = eventBooking.ElectricalCompany;


            ViewBag.Pay_Event_BookingID = new SelectList(db.payment_Booking, "Pay_Event_BookingID", "Payment_Type", eventBooking.Pay_Event_BookingID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventType1", eventBooking.EventTypeID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "Payment_Status", eventBooking.PaymentID);
            ViewBag.Service_Company_ID = new SelectList(db.service_company, "Service_Company_ID", "Service_Company1", eventBooking.Service_Company_ID);
            ViewBag.servicetypeid = new SelectList(db.serviceTypes, "ServiceTypeID", "ServiceType1", eventBooking.servicetypeid);
            ViewBag.Venue_ID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", eventBooking.Venue_ID);
            return View(eventBooking);
        }

        // GET: EventBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventBooking = db.EventBookings.Find(id);
            if (eventBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pay_Event_BookingID = new SelectList(db.payment_Booking, "Pay_Event_BookingID", "Payment_Type", eventBooking.Pay_Event_BookingID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventType1", eventBooking.EventTypeID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "Payment_Status", eventBooking.PaymentID);
            ViewBag.Service_Company_ID = new SelectList(db.service_company, "Service_Company_ID", "Service_Company1", eventBooking.Service_Company_ID);
            ViewBag.servicetypeid = new SelectList(db.serviceTypes, "ServiceTypeID", "ServiceType1", eventBooking.servicetypeid);
            ViewBag.Venue_ID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", eventBooking.Venue_ID);
            return View(eventBooking);
        }

        // POST: EventBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_ID,Event_Name,Start_Date,End_Date,No_Of_Participants,Pay_Event_BookingID,Venue_ID,servicetypeid,PaymentID,EventTypeID,Service_Company_ID,Descriptions,EventType,Venue,FoodCompany,ElectricalCompany")] EventBooking eventBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pay_Event_BookingID = new SelectList(db.payment_Booking, "Pay_Event_BookingID", "Payment_Type", eventBooking.Pay_Event_BookingID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventType1", eventBooking.EventTypeID);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "Payment_Status", eventBooking.PaymentID);
            ViewBag.Service_Company_ID = new SelectList(db.service_company, "Service_Company_ID", "Service_Company1", eventBooking.Service_Company_ID);
            ViewBag.servicetypeid = new SelectList(db.serviceTypes, "ServiceTypeID", "ServiceType1", eventBooking.servicetypeid);
            ViewBag.Venue_ID = new SelectList(db.Venues, "Venue_ID", "Venue_Name", eventBooking.Venue_ID);
            return View(eventBooking);
        }

        // GET: EventBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventBooking = db.EventBookings.Find(id);
            if (eventBooking == null)
            {
                return HttpNotFound();
            }
            return View(eventBooking);
        }

        // POST: EventBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventBooking eventBooking = db.EventBookings.Find(id);
            db.EventBookings.Remove(eventBooking);
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


        public ActionResult MyAction(string button)
        {
            return RedirectToAction("Index", "Date");
        }

        public ActionResult Summary()
        {
           EventBooking eb = (from record in db.EventBookings
                              orderby record.Event_ID descending
                              select record).First();

           
            return View(eb);
        }

        [HttpGet]
        public ActionResult Payments()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payments(Payment obj)
        {
            CreditcardValidation cchk = new CreditcardValidation();
           if(cchk.CardCheck(obj.CardNumber))
            {
                db.Payments.Add(obj);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Success = "Payment Successful!!!!";
                return View(obj);
               


            }
            else
            {
                ViewBag.Error = "Invalid Card Number!!!!";
                return View(obj);
            }
         

        }

        public ActionResult Articles()
        {
            return View();
        }



    }
}
