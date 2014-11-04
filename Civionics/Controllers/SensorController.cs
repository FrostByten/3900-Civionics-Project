using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Civionics.Models;
using Civionics.DAL;

namespace Civionics.Controllers
{
    public class SensorController : Controller
    {
        private CivionicsContext db = new CivionicsContext();

        //
        // GET: /Sensor/List
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData.Add("projectid", id);

            return View(db.Sensors.Where(k => k.ProjectID == id).OrderByDescending(k => k.Status));
        }

        // GET: /Sensor/Create
        public ActionResult Create(int? id)
        {
            Sensor s = new Sensor();
            s.TypeID = 0;
            s.ProjectID = (id == null ? 0 : (int)id);
            List<String> list = new List<String>();
            List<SensorType> types = db.Types.ToList<SensorType>();
            for(int i = 0; i < types.Count; i ++)
            {
                list.Add(types[i].Type + ":" + types[i].Units);
            }

            ViewBag.typelist = list;
            
            return View(s);
        }

        // POST: /Sensor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,TypeID,SiteID,MinSafeReading,MaxSafeReading")] Sensor sensor, int? typeselectlist)
        {
            System.Diagnostics.Debug.WriteLine(sensor.TypeID);
            System.Diagnostics.Debug.WriteLine(typeselectlist);
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine(sensor.TypeID);
                sensor.TypeID = (int)typeselectlist;
                sensor.Status = SensorStatus.Safe;
                db.Sensors.Add(sensor);
                db.SaveChanges();
                return RedirectToAction("List/" + sensor.ProjectID);
            }

            List<String> list = new List<String>();
            List<SensorType> types = db.Types.ToList<SensorType>();
            for (int i = 0; i < types.Count; i++)
            {
                list.Add(types[i].Type + ":" + types[i].Units);
            }

            ViewBag.typelist = list;

            return View(sensor);
        }

        // GET: /Sensor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Readings.RemoveRange(db.Readings.Where(k => k.SensorID == id));
            db.Sensors.Remove(db.Sensors.Find(id));

            db.SaveChanges();
            return RedirectToAction("../Project/Index");
        }

        // GET: /Sensor/New_Type
        public ActionResult New_Type()
        {
            return View();
        }

        // POST: /Sensor/New_Type
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New_Type([Bind(Include = "Type, Units")] SensorType sensortype)
        {
            if (ModelState.IsValid)
            {
                db.Types.Add(sensortype);
                db.SaveChanges();
                return RedirectToAction("../Project/Index");
            }

            return View(sensortype);
        }
	}
}