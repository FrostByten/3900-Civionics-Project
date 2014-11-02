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
            s.ProjectID = (id == null ? 0 : (int)id);

            return View(s);
        }

        // POST: /Sensor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,SensorTypeID,SiteID,UnitID,MinSafeReading,MaxSafeReading")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                sensor.Status = SensorStatus.Safe;
                db.Sensors.Add(sensor);
                db.SaveChanges();
                return RedirectToAction("List/" + sensor.ProjectID);
            }

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
	}
}