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
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Description")] Project project)
        {
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /Project/Delete/5
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
        public ActionResult Add(int? id)
        {
            ProjectAccess pa = new ProjectAccess();
            pa.ProjectID = (id == null ? 0 : (int)id);

            return View(pa);
        }

        // POST: /Project/Add
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

            return View(projectaccess);
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
