using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DrinkPlanerWebservice.Models;

namespace DrinkPlanerWebservice.Controllers.API
{
    public class GetraenksController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Getraenks
        public IQueryable<Getraenk> GetGetraenks()
        {
            return db.Getraenks;
        }

        // GET: api/Getraenks/5
        [ResponseType(typeof(Getraenk))]
        public IHttpActionResult GetGetraenk(int id)
        {
            Getraenk getraenk = db.Getraenks.Find(id);
            if (getraenk == null)
            {
                return NotFound();
            }

            return Ok(getraenk);
        }

        // PUT: api/Getraenks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGetraenk(int id, Getraenk getraenk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getraenk.Id)
            {
                return BadRequest();
            }

            db.Entry(getraenk).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GetraenkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Getraenks
        [ResponseType(typeof(Getraenk))]
        public IHttpActionResult PostGetraenk(Getraenk getraenk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Getraenks.Add(getraenk);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = getraenk.Id }, getraenk);
        }

        // DELETE: api/Getraenks/5
        [ResponseType(typeof(Getraenk))]
        public IHttpActionResult DeleteGetraenk(int id)
        {
            Getraenk getraenk = db.Getraenks.Find(id);
            if (getraenk == null)
            {
                return NotFound();
            }

            db.Getraenks.Remove(getraenk);
            db.SaveChanges();

            return Ok(getraenk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GetraenkExists(int id)
        {
            return db.Getraenks.Count(e => e.Id == id) > 0;
        }
    }
}