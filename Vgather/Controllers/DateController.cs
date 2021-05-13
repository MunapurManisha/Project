using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using System.Data.Entity;
using Vgather.Models;

namespace Vgather.Controllers
{
    public class DateController : Controller
    {
        public EventManagementEntities db = new EventManagementEntities();
        private object eventBooking;

        // GET: Date
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;

            scheduler.Config.first_hour = 6;
            scheduler.Config.last_hour = 20;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }


        public ContentResult Data()
        {
            var apps = (from e in db.EventBookings.Where(s => s.Start_Date != null && s.End_Date != null)
                        select new
                        {
                            
                            id = e.Event_ID,
                            text = e.Descriptions,
                            start_date = e.Start_Date,
                            end_date = e.End_Date
                        }).ToList();
            //List<SchedulerEvent> events = new List<SchedulerEvent>()
            //{
            //    new SchedulerEvent()
            //    {
            //        id = 1,
            //        text = "Event 1",
            //        start_date = DateTime.Now.AddHours(-1),
            //        end_date = DateTime.Now.AddHours(1)
            //    }

            //};


            return new SchedulerAjaxData(apps);
        }

        public class SchedulerEvent
        {
            public int id { get; set; }
            public string text { get; set; }
            public DateTime start_date { get; set; }
            public DateTime end_date { get; set; }

        }


        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = DHXEventsHelper.Bind<EventBooking>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                   
                        db.EventBookings.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        db.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:// "update"  
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
          

                db.SaveChanges();
                action.TargetId = changedEvent.Event_ID;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
            
        }

        private object RedirectToAction(Func<ActionResult> index)
        {
            throw new NotImplementedException();
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