using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrinkPlaner.Model;
using DrinkPlanerWebservice.Models;

namespace DrinkPlanerWebservice.Controllers.Website
{
    public class PackageUnitsController : Controller
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: PackageUnits
        public ActionResult Index()
        {
            return View(db.PackageUnits.ToList());
        }

        // GET: PackageUnits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageUnit packageUnit = db.PackageUnits.Find(id);
            if (packageUnit == null)
            {
                return HttpNotFound();
            }
            return View(packageUnit);
        }

        // GET: PackageUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackageUnits/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Unit,Value")] PackageUnit packageUnit)
        {
            if (ModelState.IsValid)
            {
                db.PackageUnits.Add(packageUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(packageUnit);
        }

        // GET: PackageUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageUnit packageUnit = db.PackageUnits.Find(id);
            if (packageUnit == null)
            {
                return HttpNotFound();
            }
            return View(packageUnit);
        }

        // POST: PackageUnits/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Unit,Value")] PackageUnit packageUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packageUnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(packageUnit);
        }

        // GET: PackageUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageUnit packageUnit = db.PackageUnits.Find(id);
            if (packageUnit == null)
            {
                return HttpNotFound();
            }
            return View(packageUnit);
        }

        // POST: PackageUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageUnit packageUnit = db.PackageUnits.Find(id);
            db.PackageUnits.Remove(packageUnit);
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
