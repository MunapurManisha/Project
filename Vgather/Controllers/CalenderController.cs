//using System;
//using System.Linq;
//using System.Web.Mvc;
//using System.Data.Entity;
//using DHTMLX.Scheduler;
//using Vgather.Models;
//using DHTMLX.Scheduler.Data;
//using DHTMLX.Common;

//namespace Vgather.Controllers
//{
//    public class CalenderController : Controller
//    {

//        private EventManagementEntities1 db = new EventManagementEntities1();
//        // GET: Calender
//        public ActionResult Index()
//        {
//            var scheduler = new DHXScheduler(this);
//            scheduler.Skin = DHXScheduler.Skins.Flat;
//            scheduler.Config.first_hour = 6;
//            scheduler.Config.last_hour = 20;

//            scheduler.LoadData = true;
//            scheduler.EnableDataprocessor = true;


//            return View(scheduler);
//        }

//        public ContentResult Data()
//        {
//            var apps = db.EventBookings.ToList();
//            return new SchedulerAjaxData(apps);
//        }

//        public ActionResult Save(int? id, FormCollection actionValues)
//        {
//            var action = new DataAction(actionValues);

//            try
//            {
//                var changedEvent = DHXEventsHelper.Bind<EventBooking>(actionValues);
//                switch (action.Type)
//                {
//                    case DataActionTypes.Insert:
//                        db.EventBookings.Add(changedEvent);
//                        break;
//                    case DataActionTypes.Delete:
//                        db.Entry(changedEvent).State = EntityState.Deleted;
//                        break;
//                    default:// "update"  
//                        db.Entry(changedEvent).State = EntityState.Modified;
//                        break;
//                }
//                db.SaveChanges();
//                action.TargetId = changedEvent.Event_ID;
//            }
//            catch (Exception a)
//            {
//                action.Type = DataActionTypes.Error;
//            }

//            return (new AjaxSaveResponse(action));
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//    }
//}