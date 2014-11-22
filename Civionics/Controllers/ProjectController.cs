using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Civionics.Models;
using Civionics.DAL;

namespace Civionics.Controllers
{
    public class ProjectController : Controller
    {
        public ProjectController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public ProjectController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        private CivionicsContext db = new CivionicsContext();
        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: /Project/
        /// <summary>
        /// Gives a list of projects and associated data
        /// Gives a list of all to admins, and
        /// a list of permitted to users
        /// </summary>
        /// <returns>A list of project models</returns>
        public ActionResult Index()
        {
            if (User.Identity.Name == "admin")
            {
                return View(db.Projects.OrderByDescending(k => k.Status));
            }
            else
            {
                return View(db.Projects.Where(k=>db.ProjectAccesses.Any(j=>j.ProjectID==k.ID && j.UserName == User.Identity.Name)).OrderByDescending(k => k.Status));
            }
        }

        // GET: /Project/Details/5
        /// <summary>
        /// Gives the details of a particular project, based on the given id
        /// </summary>
        /// <param name="id">The id of the model to give details for</param>
        /// <returns>The project with the specified id</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        /// <summary>
        /// Displays the empty create page
        /// </summary>
        /// <returns>The view for the initial create page</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Project/Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project">The project model returned from the create page</param>
        /// <returns>If the model is valid, redirects to index. If model is invalid, 
        /// returns the model to the create view and displays the associated error</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Description")] Project project)
        {
            if(project.Name == null || project.Description == null)
            {
                ModelState.AddModelError("", "All fields are required.");
                return View(project);
            }
            if (ModelState.IsValid)
            {
                project.DateAdded = DateTime.Now;
                project.Status = ProjectStatus.Safe;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: /Project/Edit/5
        /// <summary>
        /// Displays the empty edit page filled with the uneditable values
        /// </summary>
        /// <param name="id">The id of the project to be edited</param>
        /// <returns>The view for the initial edit page populated with
        /// the values of the project</returns>
        public ActionResult Edit(int? id)
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

            return View(db.ProjectAccesses.Where(k=>k.ProjectID == id));
        }

        // POST: /Project/Edit/5
        /// <summary>
        /// Validates project data and edits projects based on user input
        /// </summary>
        /// <param name="project">The modified project model</param>
        /// <returns>If invalid, re-display view with error. If valid,
        /// redirect to index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Description,Status,DateAdded")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Project/Delete/5
        /// <summary>
        /// Asks a user to confirm deletion of a project
        /// </summary>
        /// <param name="id">The id of the project to delete</param>
        /// <returns>The confirmation view filled with the project data</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        /// <summary>
        /// Deletes a project after confirmation
        /// </summary>
        /// <param name="id">The id of the project to delete</param>
        /// <returns>Redirects to index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.ProjectAccesses.RemoveRange(db.ProjectAccesses.Where(k => k.ProjectID == id)); //Remove accesses associated with the project
            db.Sensors.Where(k => k.ProjectID == id).ForEachAsync(j => db.Readings.RemoveRange(db.Readings.Where(l => l.SensorID == j.ID))); //Remove readings for all sensors associated with the project
            db.Sensors.RemoveRange(db.Sensors.Where(k => k.ProjectID == id)); //Remove all sensors associated with the project
            db.Projects.Remove(db.Projects.Find(id));

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Project/DeleteAccess/5
        /// <summary>
        /// Asks the user to confirm deletion of a project permission
        /// </summary>
        /// <param name="id">The id of the projectaccess to delete</param>
        /// <returns>The delete view filled with the projectaccess data</returns>
        public ActionResult DeleteAccess(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectAccess pa = db.ProjectAccesses.Find(id);
            if (pa == null)
            {
                return HttpNotFound();
            }
            return View(pa);
        }

        // POST: /Project/DeleteAccess/5
        /// <summary>
        /// Deletes a projectaccess after confirmation
        /// </summary>
        /// <param name="id">The id of the projectaccess to delete</param>
        /// <returns>Redirects to index</returns>
        [HttpPost, ActionName("DeleteAccess")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccessConfirmed(int id)
        {
            ProjectAccess pa = db.ProjectAccesses.Find(id);
            db.ProjectAccesses.RemoveRange(db.ProjectAccesses.Where(k => k.ProjectID == pa.ProjectID && k.UserName == pa.UserName));

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Project/Add/5
        /// <summary>
        /// Displays the projectaccess add view
        /// </summary>
        /// <param name="id">The project to create a projectaccess for</param>
        /// <returns>Add view filled with the list of users</returns>
        public ActionResult Add(int? id)
        {
            ProjectAccess pa = new ProjectAccess();
            pa.ProjectID = (id == null ? 0 : (int)id);

            List<String> list = new List<String>();
            ApplicationDbContext dbc = new ApplicationDbContext();
            List<ApplicationUser> users = dbc.Users.ToList<ApplicationUser>();
            users.Remove(users.Where(k => k.UserName == "admin").First());

            ViewBag.userselect = new SelectList(users, "Username", "Username", pa.UserName);

            return View(pa);
        }

        // POST: /Project/Add
        /// <summary>
        /// Adds a projectaccess after confirmation
        /// </summary>
        /// <param name="projectaccess">The projectaccess model to add to the database</param>
        /// <returns>If invalid, returns the view and the associated error, if valid
        /// redirects to index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ProjectID, UserName")] ProjectAccess projectaccess)
        {
            ApplicationUser au = UserManager.FindByName(projectaccess.UserName);
            bool exists = db.ProjectAccesses.Any(k => (k.UserName == projectaccess.UserName) && (k.ProjectID == projectaccess.ProjectID));
            if (ModelState.IsValid && au!=null && !exists)
            {
                db.ProjectAccesses.Add(projectaccess);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if(au==null)
            {
                ModelState.AddModelError("", "No such user exists!");
            }
            else if(exists)
            {
                ModelState.AddModelError("", "User already has permission for this project!");
            }

            List<String> list = new List<String>();
            ApplicationDbContext dbc = new ApplicationDbContext();
            List<ApplicationUser> users = dbc.Users.ToList<ApplicationUser>();
            users.Remove(users.Where(k => k.UserName == "admin").First());

            ViewBag.userselect = new SelectList(users, "Id", "Username", projectaccess.UserName);

            return View(projectaccess);
        }

        /// <summary>
        /// Disposes of the database context
        /// </summary>
        /// <param name="disposing">Whether or not to dispose of the context</param>
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
