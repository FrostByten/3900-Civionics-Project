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
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace Civionics.Controllers
{   
    /// <summary>
    /// File: ReadingController.cs
    /// 
    /// Created by: Lewis Scott
    /// Edited by: Sanders Lee
    /// 
    /// Class: ReadingController
    /// 
    /// Data Members:
    ///     CivionicsContext db     // accesses the database
    /// 
    /// Methods:
    ///     ActionResult Table(int? id, int? num)   // displays a page with the data in a table
    ///     ActionResult ChartDisplay(int? id)      // displays a page with the data in a graph
    ///     ActionResult Chart(int? id, int? num)   // produces a graph from the data
    /// 
    /// Description:
    ///     Displays the data from a sensor in two different formats, each in a separate page.
    /// </summary> 
    public class ReadingController : Controller
    {
        private CivionicsContext db = new CivionicsContext();

        // GET: /Reading/Table
        /// <author>
        /// Lewis Scott
        /// </author>
        /// <summary>
        /// Gets a given number of readings in date order since before now
        /// for a given sensor
        /// </summary>
        /// <param name="id">The id of the sensor to get readings for</param>
        /// <param name="num">The number of readings to return</param>
        /// <returns>The view filled with readings from the sensor</returns>
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

        // GET: /Reading/ChartDisplay
        /// <author>
        /// Lewis Scott
        /// </author>
        /// <summary>
        /// Displays the view filled with data relevant to the given sensor
        /// </summary>
        /// <param name="id">The id of the sensor to get data for</param>
        /// <returns>The View filled with data relevant to the given sensor</returns>
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

        // GET: /Reading/Chart
        /// <author>
        /// Sanders Lee
        /// </author>
        /// <summary>
        /// Renders the graph of a sensor's data
        /// </summary>
        /// <param name="id">The id of the sensor to get readings for</param>
        /// <param name="num">The number of latest readings to get</param>
        /// <returns>Returns the graph in png format</returns>
        public ActionResult Chart(int? id, int? num)
        {
            if (id == null || num == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Sensor s = db.Sensors.Find(id);

            if (s == null)
                return HttpNotFound();

            System.Web.UI.DataVisualization.Charting.Chart chart =
                new System.Web.UI.DataVisualization.Charting.Chart();

            chart.Width = 1100;
            chart.Height = 470;
            chart.AntiAliasing = System.Web.UI.DataVisualization.Charting.AntiAliasingStyles.Graphics;
            chart.BackImageTransparentColor = System.Drawing.Color.Transparent;
            chart.BackColor = System.Drawing.Color.Transparent;
            chart.ChartAreas.Add(new ChartArea());

            // label the axes
            chart.ChartAreas[0].AxisX.Title = "Time";
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12f, FontStyle.Bold);
            chart.ChartAreas[0].AxisY.Title = s.Type.Type + " (" + s.Type.Units + ")";
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12f, FontStyle.Bold);
            chart.ChartAreas[0].BackImageTransparentColor = System.Drawing.Color.Transparent;
            chart.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;

            for (int i = 0; i < 4; i++)
                chart.Series.Add(new Series());

            // declarations and stylings for lines and points
            // main data line
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.Series[0].Color = Color.Navy;
            chart.Series[0].BorderWidth = 2;
            // upper limit line
            chart.Series[1].ChartType = SeriesChartType.Line;
            chart.Series[1].Color = Color.DarkRed;
            chart.Series[1].BorderWidth = 3;
            chart.Series[1].BorderDashStyle = ChartDashStyle.Dash;
            // lower limit line
            chart.Series[2].ChartType = SeriesChartType.Line;
            chart.Series[2].Color = Color.DarkRed;
            chart.Series[2].BorderWidth = 3;
            chart.Series[2].BorderDashStyle = ChartDashStyle.Dash;
            // data point highlight
            chart.Series[3].ChartType = SeriesChartType.Point;
            chart.Series[3].MarkerSize = 10;
            

            // set the graph data, and only get the specified number of most recent points
            List<Reading> list = db.Readings.Where(k => k.SensorID == id).OrderBy(k => k.ID).ToList();

            // no data == no image
            if (list.Count == 0)
                return null;

            // cannot grab more data points than there is
            if (num > list.Count)
                num = list.Count;

            for (int i = (int)num - 1; i >=0; i--)
            {
                string x = list[list.Count - i - 1].LoggedTime.ToString();
                double y = list[list.Count - i - 1].Data;

                chart.Series[0].Points.AddXY(x, y); // data line
                chart.Series[1].Points.AddXY(x, s.MaxSafeReading); // upper limit line
                chart.Series[2].Points.AddXY(x, s.MinSafeReading); // lower limit line
                chart.Series[3].Points.AddXY(x, y); // data points

                // color the points differently depending on abnormality
                if (y < s.MinSafeReading || y > s.MaxSafeReading)
                    chart.Series[3].Points[(int)num - i - 1].Color = Color.Red;
                else
                    chart.Series[3].Points[(int)num - i - 1].Color = Color.Aqua;
            }

            // labelling the max and min lines
            chart.Series[1].Points[0].Label = "Maximum Acceptable Value";
            chart.Series[1].Points[0].LabelForeColor = Color.DarkRed;
            chart.Series[2].Points[0].Label = "Minimum Acceptable Value";
            chart.Series[2].Points[0].LabelForeColor = Color.DarkRed;

            // draw the graph out to a file
            using (var ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms.ToArray(), "image/png", "datachart.png");
            }
        }

	}
}