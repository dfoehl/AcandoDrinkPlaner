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
using DrinkPlanerWebservice.Models;

namespace DrinkPlanerWebservice.Controllers.API
{
    public class PartiesController : ApiController
    {
        private DrinkPlanerWebserviceContext db = new DrinkPlanerWebserviceContext();

        // GET: api/Parties
        public IQueryable<Party> GetParties()
        {
            return db.Parties.Include(p => p.GetraenkeBedarf.Select(b => b.Getraenk));
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