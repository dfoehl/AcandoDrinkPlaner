﻿using System;
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
    public class PartiesController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Parties
        public IQueryable<Party> GetParties()
        {
            return db.Parties
                .Include(p => p.NeededDrinks.Select(b => b.Drink.PackageUnit))
                .Include(p => p.Guests.Select(g => g.Person))
                .Include(p => p.Guests.Select(g => g.Supply));
        }

        // GET: api/Parties?forPersonId
        public IQueryable<Party> GetPartiesForPerson(int forPersonId)
        {
            return db.Parties.Where(p => p.Creator.Id == forPersonId || p.Guests.Any(pp => pp.Person.Id == forPersonId))
                .Include(p => p.NeededDrinks.Select(b => b.Drink.PackageUnit))
                .Include(p => p.Guests.Select(g => g.Person))
                .Include(p => p.Guests.Select(g => g.Supply));
        }

        // GET: api/Parties/5
        [ResponseType(typeof(Party))]
        public IHttpActionResult GetParty(int id)
        {
            Party party = db.Parties.Find(id);
            if (party == null)
            {
                return NotFound();
            }

            return Ok(party);
        }

        // PUT: api/Parties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParty(int id, Party party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != party.Id)
            {
                return BadRequest();
            }

            db.Entry(party).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyExists(id))
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

        // POST: api/Parties
        [ResponseType(typeof(Party))]
        public IHttpActionResult PostParty(Party party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (party?.Creator?.Id != 0)
            {
                party.Creator = db.People.Find(party.Creator.Id);
            }
            db.Parties.Add(party);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = party.Id }, party);
        }

        // DELETE: api/Parties/5
        [ResponseType(typeof(Party))]
        public IHttpActionResult DeleteParty(int id)
        {
            Party party = db.Parties.Find(id);
            if (party == null)
            {
                return NotFound();
            }

            db.Parties.Remove(party);
            db.SaveChanges();

            return Ok(party);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartyExists(int id)
        {
            return db.Parties.Count(e => e.Id == id) > 0;
        }
    }
}