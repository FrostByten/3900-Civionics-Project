using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _3900_Civionics.Models;

namespace _3900_Civionics.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();
        private ProjectAccessDBContext dbaccess = new ProjectAccessDBContext();

        // GET: /Project/
        public ActionResult Index()
        {
            List<Project> o = new List<Project>();
            List<Project> list = db.Projects.ToList();
            List<ProjectAccess> access = dbaccess.ProjectAccessList.ToList();

            System.Diagnostics.Debug.WriteLine("\n\n\nAccessing user permissions...\n\n");

            for (int i = 0; i < list.Count; i++)
            {
                if(User.Identity.Name == "admin")
                {
                    o.Add(list[i]);
                    continue;
                }
                else
                {
                    for (int j = 0; j < access.Count; j++)
                    {
                        if ((list[i].ID == access[j].ProjectID) && (access[j].UserName == User.Identity.Name))
                        {
                            o.Add(list[i]);
                            break;
                        }
                    }
                }
            }

            return View(o);
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
        public ActionResult Create([Bind(Include="ID,Name,Description,DateAdded")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.DateAdded = DateTime.Now;
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
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Description,DateAdded")] Project project)
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
            List<ProjectAccess> access = dbaccess.ProjectAccessList.ToList();

            for(int i = 0; i < access.Count; i++)
            {
                if(access[i].ProjectID == id)
                {
                    dbaccess.ProjectAccessList.Remove(access[i]);
                }
            }

            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
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
