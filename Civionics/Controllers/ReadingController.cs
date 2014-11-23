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
using System.Web.Helpers;

namespace Civionics.Controllers
{
    public class ReadingController : Controller
    {
        private CivionicsContext db = new CivionicsContext();

        //
        // GET: /Reading/Table
        public ActionResult Table(int? id, int? num)
        {
            if (id == null || num == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor s = db.Sensors.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }

            ViewData.Add("projectid", s.ProjectID);
            ViewData.Add("sensorid", s.ID);
            ViewData.Add("units", s.Type.Units);
            ViewData.Add("type", s.Type.Type);
            ViewData.Add("min", s.MinSafeReading);
            ViewData.Add("max", s.MaxSafeReading);
            ViewData.Add("site", s.SiteID);
            ViewData.Add("projectname", s.Project.Name);
            ViewData.Add("sensorserial", s.serial);
            ViewData.Add("num", num);

            List<Reading> list = db.Readings.Where(k => k.SensorID == id).OrderByDescending(k => k.ID).ToList();
            if (list.Count < num)
                return View(list.OrderBy(k => k.LoggedTime));
            else
            {
                List<Reading> o = new List<Reading>();
                for (int i = 0; i < num; i++)
                {
                    o.Add(list[i]);
                }
                return View(o.OrderBy(k => k.LoggedTime));
            }
        }

        //
        // GET: /Reading/ChartDisplay
        public ActionResult ChartDisplay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor s = db.Sensors.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }

            ViewData.Add("projectid", s.ProjectID);
            ViewData.Add("sensorid", s.ID);
            ViewData.Add("units", s.Type.Units);
            ViewData.Add("type", s.Type.Type);
            ViewData.Add("min", s.MinSafeReading);
            ViewData.Add("max", s.MaxSafeReading);
            ViewData.Add("site", s.SiteID);
            ViewData.Add("projectname", s.Project.Name);
            ViewData.Add("sensorserial", s.serial);

            return View();
        }

        //
        // GET: /Reading/Chart
        public ActionResult Chart(int? id, int? num)
        {
            if (id == null || num == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Sensor s = db.Sensors.Find(id);

            if (s == null)
                return HttpNotFound();

            ViewData.Add("units", s.Type.Units);
            ViewData.Add("min", s.MinSafeReading);
            ViewData.Add("max", s.MaxSafeReading);

            List<Reading> list = db.Readings.Where(k => k.SensorID == id).OrderByDescending(k => k.ID).ToList();
            if(list.Count < num)
                return View(list.OrderBy(k => k.LoggedTime));
            else
            {
                List<Reading> o = new List<Reading>();
                for(int i = 0; i < num; i++)
                {
                    o.Add(list[i]);
                }
                return View(o.OrderBy(k => k.LoggedTime));
            }
        }

	}
}