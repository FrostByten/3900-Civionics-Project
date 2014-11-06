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
            Project p = db.Projects.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            ViewData.Add("projectid", id);

            return View(db.Sensors.Where(k => k.ProjectID == id).OrderByDescending(k => k.Status));
        }

        // GET: /Sensor/Create
        public ActionResult Create(int? id)
        {
            Project p = db.Projects.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            Sensor s = new Sensor();
            s.TypeID = 0;
            s.ProjectID = (id == null ? 0 : (int)id);
            List<String> list = new List<String>();
            List<SensorType> types = db.Types.ToList<SensorType>();
            for(int i = 0; i < types.Count; i ++)
            {
                list.Add(types[i].Type + ":" + types[i].Units);
            }
            ViewBag.typeselect = new SelectList(db.Types, "ID", "Type", s.TypeID);
            ViewData.Add("projectid", id.ToString());
            
            return View(s);
        }

        // POST: /Sensor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,TypeID,SiteID,MinSafeReading,MaxSafeReading,AutoRange,AutoPercent")] Sensor sensor)
        {
            List<String> list = new List<String>();
            List<SensorType> types = db.Types.ToList<SensorType>();
            for (int i = 0; i < types.Count; i++)
            {
                list.Add(types[i].Type + ":" + types[i].Units);
            }
            ViewBag.typeselect = new SelectList(db.Types, "ID", "Type", sensor.TypeID);

            if(sensor.MaxSafeReading <= sensor.MinSafeReading)
            {
                ModelState.AddModelError("", "Max reading must be larger than min reading.");
                return View(sensor);
            }
            if(sensor.SiteID == null)
            {
                ModelState.AddModelError("", "Site ID is a required field.");
                return View(sensor);
            }
            sensor.Status = SensorStatus.Safe;
            if (!sensor.AutoRange)
            {
                sensor.AutoPercent = 0;
            }

            if (ModelState.IsValid)
            {
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
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ViewData.Add("projectid", sensor.ProjectID.ToString());

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

        // GET: /Sensor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sensor s = db.Sensors.Find(id);
            if (s == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewData.Add("sensorid", id);
            ViewData.Add("projectid", s.ProjectID.ToString());

            return View(s);
        }

        // POST: /Sensor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TypeID,SiteID,MinSafeReading,MaxSafeReading,AutoRange,AutoPercent")] Sensor sensor)
        {
            if(sensor.MaxSafeReading <= sensor.MinSafeReading)
            {
                ModelState.AddModelError("", "Max reading must be larger than min reading.");
                return View();
            }
            if (ModelState.IsValid)
            {
                Sensor o = db.Sensors.Find(sensor.ID);
                if (o == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                o.MinSafeReading = sensor.MinSafeReading;
                o.MaxSafeReading = sensor.MaxSafeReading;
                o.AutoRange = sensor.AutoRange;

                if(sensor.AutoRange)
                {
                    o.AutoPercent = sensor.AutoPercent;
                }
                else
                {
                    o.AutoPercent = 0;
                }

                db.SaveChanges();
                return RedirectToAction("List/" + o.ProjectID);
            }
            return View();
        }
	}
}