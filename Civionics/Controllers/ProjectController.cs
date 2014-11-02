using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Civionics.Models;
using Civionics.DAL;

namespace Civionics.Controllers
{
    public class ProjectController : Controller
    {
        private CivionicsContext db = new CivionicsContext();

        // GET: /Project/
        public ActionResult Index()
        {
            if (User.Identity.Name == "admin")
            {
                return View(db.Projects.ToList());
            }
            else
            {
                return View(db.Projects.Where(k=>db.ProjectAccesses.Any(j=>j.ProjectID==k.ID && j.UserName == User.Identity.Name)).ToList());
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

            List<ProjectAccess> palist = db.ProjectAccesses.ToList();
            List<ProjectAccess> o = new List<ProjectAccess>();

            for (int i = 0; i < palist.Count; i++)
            {
                if (palist.ElementAt(i).ProjectID == id)
                {
                    o.Add(palist.ElementAt(i));
                }
            }

            ViewData.Add("projectid", id);

            return View(o);
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
            List<ProjectAccess> access = db.ProjectAccesses.ToList();

            for (int i = 0; i < access.Count; i++)
            {
                if (access[i].ProjectID == id)
                {
                    db.ProjectAccesses.Remove(access[i]);
                }
            }

            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
            List<ProjectAccess> access = db.ProjectAccesses.ToList();

            for (int i = 0; i < access.Count; i++)
            {
                if (access[i].ProjectAccessID == id)
                {
                    db.ProjectAccesses.Remove(access[i]);
                }
            }
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
            if (ModelState.IsValid)
            {
                db.ProjectAccesses.Add(projectaccess);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(projectaccess.ProjectAccessID + ", " + projectaccess.ProjectID + ", " + projectaccess.UserName);
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
