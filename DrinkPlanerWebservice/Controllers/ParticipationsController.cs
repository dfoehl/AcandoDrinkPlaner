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
using DrinkPlaner.Model;
using DrinkPlanerWebservice.Models;

namespace DrinkPlanerWebservice.Controllers
{
    public class ParticipationsController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Participations
        public IQueryable<Participation> GetParticipations()
        {
            return db.Participations;
        }

        // GET: api/Participations/5
        [ResponseType(typeof(Participation))]
        public IHttpActionResult GetParticipation(int id)
        {
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return NotFound();
            }

            return Ok(participation);
        }

        // PUT: api/Participations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipation(int id, Participation participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participation.Id)
            {
                return BadRequest();
            }

            db.Entry(participation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipationExists(id))
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

        // POST: api/Participations
        [ResponseType(typeof(Participation))]
        public IHttpActionResult PostParticipation(Participation participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Participations.Add(participation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = participation.Id }, participation);
        }

        // DELETE: api/Participations/5
        [ResponseType(typeof(Participation))]
        public IHttpActionResult DeleteParticipation(int id)
        {
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return NotFound();
            }

            db.Participations.Remove(participation);
            db.SaveChanges();

            return Ok(participation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipationExists(int id)
        {
            return db.Participations.Count(e => e.Id == id) > 0;
        }
    }
}