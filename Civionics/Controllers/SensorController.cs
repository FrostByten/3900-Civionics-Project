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

        // GET: /Sensor/List
        /// <summary>
        /// Gets a list of sensors for a given project
        /// </summary>
        /// <param name="id">The id of the project to get sensors for</param>
        /// <returns>The View filled with sensors</returns>
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

            ProjectAccess pa = null;
            try
            {
                pa = db.ProjectAccesses.First(k => k.ProjectID == id && k.UserName == User.Identity.Name);
            }
            catch (Exception e) 
            {
                if(!User.IsInRole("admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            if (pa == null && !User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData.Add("projectid", id);

            return View(db.Sensors.Where(k => k.ProjectID == id).OrderByDescending(k => k.Status));
        }

        // GET: /Sensor/Create
        /// <summary>
        /// Gets the initial create page
        /// </summary>
        /// <param name="id">The id to create a sensor for</param>
        /// <returns>The View filled with all the different types</returns>
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
        /// <summary>
        /// Validates user input and creates a sensor based on it
        /// </summary>
        /// <param name="sensor">The model containing the user-inputted information</param>
        /// <returns>The old View if data is invalid, otherwise redirects to the
        /// sensor list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,TypeID,SiteID,MinSafeReading,MaxSafeReading,AutoRange,AutoPercent,serial")] Sensor sensor)
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
            if ((sensor.AutoPercent <= 0 || sensor.AutoPercent > 100) && sensor.AutoRange)
            {
                ModelState.AddModelError("", "Auto Percent should be a number between 0 and 99");
                return View(sensor);
            }
            sensor.Status = SensorStatus.Safe;
            if (!sensor.AutoRange)
            {
                sensor.AutoPercent = 0;
            }

            if(sensor.serial == null)
            {
                ModelState.AddModelError("", "Serial # is a required field");
                return View(sensor);
            }

            if(!System.Text.RegularExpressions.Regex.IsMatch(sensor.serial, "^([0-9][0-9]\\-){3}[0-9][0-9]$"))
            {
                ModelState.AddModelError("", "Serial # should be in the format: ##-##-##-##");
                return View(sensor);
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
        /// <summary>
        /// Confirms with the user that they really want to delete a sensor
        /// </summary>
        /// <param name="id">The id of the sensor to delete</param>
        /// <returns>The View filled with information about the selected sensor</returns>
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
        /// <summary>
        /// Deletes a sensor and all associated readings
        /// </summary>
        /// <param name="id">The id of the sensor to delete</param>
        /// <returns>Redirects to the project listing</returns>
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
        /// <summary>
        /// Displays the page for creating a new sensor type
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult New_Type()
        {
            return View();
        }

        // POST: /Sensor/New_Type
        /// <summary>
        /// Validates user input and creates a new sensor type based on it
        /// </summary>
        /// <param name="sensortype">The user inputted data</param>
        /// <returns>If invalid, returns the View with the old user data.
        /// Else, redirects to the project list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New_Type([Bind(Include = "Type, Units")] SensorType sensortype)
        {
            List<SensorType> s = db.Types.Where(k => k.Type.Equals(sensortype.Type)).ToList();

            if (ModelState.IsValid && s.Count == 0)
            {
                db.Types.Add(sensortype);
                db.SaveChanges();
                return RedirectToAction("../Project/Index");
            }

            return View(sensortype);
        }

        // GET: /Sensor/Edit/5
        /// <summary>
        /// Displays the edit page filled with all un-editable data
        /// </summary>
        /// <param name="id">The id of the sensor to edit</param>
        /// <returns>The view for editing</returns>
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
            ViewData.Add("serial", s.serial);

            return View(s);
        }

        // POST: /Sensor/Edit/5
        /// <summary>
        /// Validates user data and edits a sensor based on it
        /// </summary>
        /// <param name="sensor">The user data</param>
        /// <returns>If data is invalid, returns the View filled with
        /// the old user data, otherwise, redirects to the project list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TypeID,SiteID,MinSafeReading,MaxSafeReading,AutoRange,AutoPercent,serial")] Sensor sensor)
        {
            Sensor o = db.Sensors.Find(sensor.ID);
            if (o == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            o.MinSafeReading = sensor.MinSafeReading;
            o.MaxSafeReading = sensor.MaxSafeReading;
            o.AutoRange = sensor.AutoRange;
            o.serial = sensor.serial;

            if (sensor.AutoRange)
            {
                o.AutoPercent = sensor.AutoPercent;
            }
            else
            {
                o.AutoPercent = 0;
            }
            ViewData.Add("projectid", o.ProjectID.ToString());
            if (ModelState.IsValid)
            {
                if (sensor.MaxSafeReading <= sensor.MinSafeReading)
                {
                    ModelState.AddModelError("", "Max reading must be larger than min reading.");
                    return View(o);
                }
                if((sensor.AutoPercent <=0 || sensor.AutoPercent >100) && sensor.AutoRange)
                {
                    ModelState.AddModelError("", "Auto Percent should be a number between 0 and 99");
                    return View(o);
                }
                if(sensor.serial == null)
                {
                    ModelState.AddModelError("", "Serial # is a required field");
                    return View(sensor);
                }

                db.SaveChanges();
                return RedirectToAction("List/" + o.ProjectID);
            }
            return View(o);
        }
	}
}