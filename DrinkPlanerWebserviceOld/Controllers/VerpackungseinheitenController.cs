using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrinkPlanerWebservice.Models;

namespace DrinkPlanerWebservice.Controllers
{
    public class VerpackungseinheitenController : Controller
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: Verpackungseinheiten
        public ActionResult Index()
        {
            return View(db.Verpackungseinheits.ToList());
        }

        // GET: Verpackungseinheiten/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verpackungseinheit verpackungseinheit = db.Verpackungseinheits.Find(id);
            if (verpackungseinheit == null)
            {
                return HttpNotFound();
            }
            return View(verpackungseinheit);
        }

        // GET: Verpackungseinheiten/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verpackungseinheiten/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Einheit,Wert")] Verpackungseinheit verpackungseinheit)
        {
            if (ModelState.IsValid)
            {
                db.Verpackungseinheits.Add(verpackungseinheit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(verpackungseinheit);
        }

        // GET: Verpackungseinheiten/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verpackungseinheit verpackungseinheit = db.Verpackungseinheits.Find(id);
            if (verpackungseinheit == null)
            {
                return HttpNotFound();
            }
            return View(verpackungseinheit);
        }

        // POST: Verpackungseinheiten/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Einheit,Wert")] Verpackungseinheit verpackungseinheit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verpackungseinheit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(verpackungseinheit);
        }

        // GET: Verpackungseinheiten/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verpackungseinheit verpackungseinheit = db.Verpackungseinheits.Find(id);
            if (verpackungseinheit == null)
            {
                return HttpNotFound();
            }
            return View(verpackungseinheit);
        }

        // POST: Verpackungseinheiten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verpackungseinheit verpackungseinheit = db.Verpackungseinheits.Find(id);
            db.Verpackungseinheits.Remove(verpackungseinheit);
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
