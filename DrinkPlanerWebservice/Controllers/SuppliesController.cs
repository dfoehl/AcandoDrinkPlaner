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
    public class SuppliesController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Supplies
        public IQueryable<Supply> GetSupplies()
        {
            return db.Supplies;
        }

        // GET: api/Supplies/5
        [ResponseType(typeof(Supply))]
        public IHttpActionResult GetSupply(int id)
        {
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return NotFound();
            }

            return Ok(supply);
        }

        // PUT: api/Supplies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupply(int id, Supply supply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supply.Id)
            {
                return BadRequest();
            }

            db.Entry(supply).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplyExists(id))
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

        // POST: api/Supplies?forParticipationId=5
        [ResponseType(typeof(Supply))]
        [ActionName("participation")]
        public IHttpActionResult PostSupplyForAction(Supply supply, int forParticipationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var participation = db.Participations.Find(forParticipationId);
            db.Entry(participation).Collection(p => p.Supply);
            db.Supplies.Add(supply);
            participation.Supply.Add(supply);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supply.Id }, supply);
        }

        // POST: api/Supplies?forPartyId=5
        [ResponseType(typeof(Supply))]
        [ActionName("party")]
        public IHttpActionResult PostSupplyForParty(Supply supply, int forPartyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var party = db.Parties.Find(forPartyId);
            db.Entry(party).Collection(p => p.NeededDrinks);
            db.Supplies.Add(supply);
            party.NeededDrinks.Add(supply);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supply.Id }, supply);
        }

        // DELETE: api/Supplies/5
        [ResponseType(typeof(Supply))]
        public IHttpActionResult DeleteSupply(int id)
        {
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return NotFound();
            }

            db.Supplies.Remove(supply);
            db.SaveChanges();

            return Ok(supply);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplyExists(int id)
        {
            return db.Supplies.Count(e => e.Id == id) > 0;
        }
    }
}