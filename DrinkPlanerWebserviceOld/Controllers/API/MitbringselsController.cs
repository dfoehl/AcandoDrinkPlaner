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
    public class MitbringselsController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Mitbringsels
        public IQueryable<Mitbringsel> GetMitbringsels()
        {
            return db.Mitbringsels;
        }

        // GET: api/Mitbringsels/5
        [ResponseType(typeof(Mitbringsel))]
        public IHttpActionResult GetMitbringsel(int id)
        {
            Mitbringsel mitbringsel = db.Mitbringsels.Find(id);
            if (mitbringsel == null)
            {
                return NotFound();
            }

            return Ok(mitbringsel);
        }

        // PUT: api/Mitbringsels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMitbringsel(int id, Mitbringsel mitbringsel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mitbringsel.Id)
            {
                return BadRequest();
            }

            db.Entry(mitbringsel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MitbringselExists(id))
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

        // POST: api/Mitbringsels
        [ResponseType(typeof(Mitbringsel))]
        public IHttpActionResult PostMitbringsel(Mitbringsel mitbringsel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mitbringsels.Add(mitbringsel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mitbringsel.Id }, mitbringsel);
        }

        // DELETE: api/Mitbringsels/5
        [ResponseType(typeof(Mitbringsel))]
        public IHttpActionResult DeleteMitbringsel(int id)
        {
            Mitbringsel mitbringsel = db.Mitbringsels.Find(id);
            if (mitbringsel == null)
            {
                return NotFound();
            }

            db.Mitbringsels.Remove(mitbringsel);
            db.SaveChanges();

            return Ok(mitbringsel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MitbringselExists(int id)
        {
            return db.Mitbringsels.Count(e => e.Id == id) > 0;
        }
    }
}