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
    public class TeilnahmenController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Teilnahmen
        public IQueryable<Teilnahme> GetTeilnahmes()
        {
            return db.Teilnahmes;
        }

        // GET: api/Teilnahmen/5
        [ResponseType(typeof(Teilnahme))]
        public IHttpActionResult GetTeilnahme(int id)
        {
            Teilnahme teilnahme = db.Teilnahmes.Find(id);
            if (teilnahme == null)
            {
                return NotFound();
            }

            return Ok(teilnahme);
        }

        // PUT: api/Teilnahmen/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeilnahme(int id, Teilnahme teilnahme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teilnahme.Id)
            {
                return BadRequest();
            }

            db.Entry(teilnahme).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeilnahmeExists(id))
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

        // POST: api/Teilnahmen
        [ResponseType(typeof(Teilnahme))]
        public IHttpActionResult PostTeilnahme(Teilnahme teilnahme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teilnahmes.Add(teilnahme);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teilnahme.Id }, teilnahme);
        }

        // DELETE: api/Teilnahmen/5
        [ResponseType(typeof(Teilnahme))]
        public IHttpActionResult DeleteTeilnahme(int id)
        {
            Teilnahme teilnahme = db.Teilnahmes.Find(id);
            if (teilnahme == null)
            {
                return NotFound();
            }

            db.Teilnahmes.Remove(teilnahme);
            db.SaveChanges();

            return Ok(teilnahme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeilnahmeExists(int id)
        {
            return db.Teilnahmes.Count(e => e.Id == id) > 0;
        }
    }
}