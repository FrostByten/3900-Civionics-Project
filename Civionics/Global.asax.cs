using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using System.Text;
using System.IO;
using Civionics.DAL;
using Civionics.Models;

namespace Civionics
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static CivionicsContext db = new CivionicsContext();

        public const bool DEBUG = true; //Whether or not to print out debug messages

        public const int START_DELAY = 60; //The delay (in seconds) to wait after application startup to launch the task threads
        public const int WATCH_PERIOD = 60; //How often the watch thread should check a folder (in seconds)
        public const int STATUS_PERIOD = 1; //How often the status thread should re-calculate project and sensor statuses (in hours)
        public const int PURGE_PERIOD = 24; //How often the server purges old data from the database (in hours)

        public const string WATCH_PATH = "\\Dropbox\\"; //The path to probe when watching
        public const string JAVA_PATH = "\\Java\\"; //The path to the java folder
        public const string JAVA_JAR = "dbinsert.jar"; //The name of the dbinsert jar file

        public const int PURGE_OFFSET = 15; //Purge offset from today in days

        public const int SENSOR_WARNING_LEVEL = 1; //The amount of anomalous readings before a sensor has a warning
        public const int SENSOR_ALERT_LEVEL = 3; //The amount of anomalous readings before a sensor has an alert
        public const int SENSOR_WARNING_WEIGHT = 1; //The weight of a sensor warning considered when re-evaluating a project
        public const int SENSOR_ALERT_WEIGHT = 3; //The weight of a sensor alert considered when re-evaluating a project
        public const int PROJECT_WARNING_LEVEL = 3; //The amount of sensor weights before a project has a warning
        public const int PROJECT_ALERT_LEVEL = 9; //The amount of sensor weights before a project has an alert

        public const int MIN_READINGS = 4; //The minimum number of recent readings required to compute status for a sensor

        private static DateTime last;
        private static string dir;
        private static string java_dir;

        /// <summary>
        /// This function acts as the main entry point for the server.
        /// 
        /// It launches the directory watcher, as well as the status
        /// and purge threads.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FileSystemWatcher watcher;

            dir = Directory.GetCurrentDirectory() + WATCH_PATH;
            java_dir = Directory.GetCurrentDirectory() + JAVA_PATH;

            if(!Directory.Exists(dir))
            {
                System.Diagnostics.Debug.WriteLine("Directory " + dir + " does not exist.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Watching directory: " + dir);
                watcher = new FileSystemWatcher();
                watcher.Path = dir;
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;
                watcher.Filter = "*.*";
                watcher.Created += new FileSystemEventHandler(watcher_Created);
                watcher.EnableRaisingEvents = true;
            }

            last = DateTime.Now;

            ThreadStart status_task = new ThreadStart(status_loop);
            ThreadStart purge_task = new ThreadStart(purge_loop);

            Thread statusupdater = new Thread(status_task);
            Thread purger = new Thread(purge_task);
            
            statusupdater.Start();
            purger.Start();

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("Current date is: " + last.ToString());

            System.Diagnostics.Debug.WriteLine("System ready...\n");
        }

        /// <summary>
        /// This function is called when a new file is created in the filesystem.
        /// 
        /// It filters the only excel files, then calls the associated java application
        /// that will open the file and insert it's values into the database.
        /// </summary>
        /// <param name="sender">The filesystemwatcher object that sent the event</param>
        /// <param name="e">The event that was broadcast</param>
        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if(DEBUG)
                System.Diagnostics.Debug.WriteLine("A new file was created at: " + e.FullPath + ".");

            char[] delim = { '.' };
            string ext = e.Name.Split(delim)[1];

            if(!(ext == "xls" || ext == "xlsx"))
            {
                if(DEBUG)
                    System.Diagnostics.Debug.WriteLine("File is not an excel file... bailing...");
                return;
            }

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("Handling file: " + e.Name);

            if(!Directory.Exists(java_dir))
            {
                if(DEBUG)
                    System.Diagnostics.Debug.WriteLine("Directory: " + java_dir.ToString() + " does not exist...\nFailed to parse file: " + e.Name);
                return;
            }

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "C:\\Program Files\\Java\\jre7\\bin\\java.exe";
            p.StartInfo.Arguments = "-jar " + java_dir.ToString() + JAVA_JAR;
            //p.StartInfo.Arguments = "-version"; //for debug
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;

            try { p.Start(); }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Java might not installed, exception: " + ex.Message);
                return;
            }

            //p.WaitForExit(); //for debug
            //System.Diagnostics.Debug.WriteLine(p.StandardOutput.ReadToEnd()); //for debug
            //System.Diagnostics.Debug.WriteLine(p.StandardError.ReadToEnd()); //for debug
        }

        /// <summary>
        /// This is the entry-point for the status thread.
        /// 
        /// Loops forever: computes statuses and then waits
        /// </summary>
        static void status_loop()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(START_DELAY + 5)); // Wait for the system to start

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("Status thread active");

            for (;;)
            {
                calculate_status();
                System.Threading.Thread.Sleep(TimeSpan.FromHours(STATUS_PERIOD)); // Wait to loop
            }
        }

        /// <summary>
        /// This is the entry-point for the purge thread.
        /// 
        /// Loops forever: purges and then waits
        /// </summary>
        static void purge_loop()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(START_DELAY + 10)); // Wait for the system to start

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("Purge thread active");

            for(;;)
            {
                purge();
                System.Threading.Thread.Sleep(TimeSpan.FromHours(PURGE_PERIOD)); //Wait to loop
            }
        }

        /// <summary>
        /// Calculates the statuses for each project and sensor.
        /// 
        /// For every project, computes the status for each sensor, then
        /// uses the average status to compute the status for the project.
        /// 
        /// For every sensor, computes the status based on the average
        /// number of recent readings that were anomalous.
        /// </summary>
        static void calculate_status()
        {
            last = DateTime.Now;

            CivionicsContext dbs = new CivionicsContext();
            CivionicsContext dbr = new CivionicsContext();

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("\nComputing statuses.");

            List<Project> projlist = db.Projects.ToList();

            for (int i = 0; i < projlist.Count; i++) // For each project
            {
                if(DEBUG)
                    System.Diagnostics.Debug.WriteLine("Project: " + projlist[i].ID);

                int projlev = 0;
                int totcount = 0;
                ProjectStatus status = ProjectStatus.Safe;
                List<Sensor> senslist = dbs.Sensors.ToList().Where(s => s.ProjectID == projlist[i].ID).ToList();

                for (int j = 0; j < senslist.Count; j++) // For each sensor in project
                {
                    if(DEBUG)
                        System.Diagnostics.Debug.WriteLine("\tSensor: " + senslist[j].ID);

                    int sencount = 0;
                    SensorStatus senstatus = SensorStatus.Safe;
                    List<Reading> readlist = dbr.Readings.ToList().Where(r => (r.SensorID == senslist[j].ID) && (r.LoggedTime > last)).ToList().OrderByDescending(r => r.LoggedTime).ToList();

                    int readcount = readlist.Count;
                    float readavg = 0;

                    for (int k = 0; k < readcount; k++)
                    {
                        if(DEBUG)
                            System.Diagnostics.Debug.WriteLine("\t\tReading: " + readlist[k].ID + ", Date: " + readlist[k].LoggedTime);
                        if (readlist[k].isAnomalous)
                            sencount++;
                        totcount++;
                        readavg += readlist[k].Data;
                    }

                    readavg = readavg / readcount;
                    projlev += sencount;
                    senstatus = (sencount > 0 ? (sencount > 2 ? SensorStatus.Alert : SensorStatus.Warning) : SensorStatus.Safe);

                    if (readcount >= MIN_READINGS)
                    {
                        senslist[j].Status = senstatus;
                        System.Diagnostics.Debug.WriteLine("\t Anomalous readings: " + sencount + "/" + readcount);
                        System.Diagnostics.Debug.WriteLine("\t Computed Status: " + senstatus.ToString());
                    }
                    else
                        System.Diagnostics.Debug.WriteLine("\t Not enough recent readings to compute sensor status: "  + readcount);


                    if (senslist[j].AutoRange && readcount >= MIN_READINGS)
                    {
                        senslist[j].MaxSafeReading = readavg * (1 + senslist[j].AutoPercent);
                        senslist[j].MinSafeReading = readavg * (-1 * (1 + (senslist[j].AutoPercent)));
                    }
                }

                if(totcount > 0)
                {
                    int total = (projlev / totcount);
                    status = total >= PROJECT_ALERT_LEVEL ? ProjectStatus.Alert : (total >= PROJECT_WARNING_LEVEL ? ProjectStatus.Warning : ProjectStatus.Safe);
                    projlist[i].Status = status;
                }

                if (DEBUG)
                    System.Diagnostics.Debug.WriteLine("Computed Status: " + projlist[i].Status.ToString() + "\n");
            }

            db.SaveChanges();
            dbs.SaveChanges();
            dbr.SaveChanges();

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("Finished computing statuses.\n");
        }

        /// <summary>
        /// Removes all readings from the database that are older
        /// than a certain date.
        /// </summary>
        /// <param name="length">The number of days before today to remove</param>
        static void purge(int length = PURGE_OFFSET)
        {
            DateTimeOffset dt = DateTimeOffset.Now.AddDays(-1 * length);

            if (DEBUG)
                System.Diagnostics.Debug.WriteLine("Purging everything before: " + dt.DateTime.ToString() + "...");

            try
            {
                db.Readings.RemoveRange(db.Readings.Where(k => k.LoggedTime < dt.DateTime));

                db.SaveChanges();
                if (DEBUG)
                    System.Diagnostics.Debug.WriteLine("Finished purging readings.");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception thrown during purge: " + ex.Message + "\n\n" + ex.StackTrace + "\n");
            }
        }
    }
}
