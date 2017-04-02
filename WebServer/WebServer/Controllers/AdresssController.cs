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
using WebServer.Models;

namespace WebServer.Controllers
{
    public class AdresssController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Adresss
        public IQueryable<Adres> GetKsiazka_adresow()
        {
            return db.Ksiazka_adresow;
        }

        // GET: api/Adresss/5
        [ResponseType(typeof(Adres))]
        public IHttpActionResult GetAdres(int id)
        {
            Adres adres = db.Ksiazka_adresow.Find(id);
            if (adres == null)
            {
                return NotFound();
            }

            return Ok(adres);
        }

        // PUT: api/Adresss/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdres(int id, Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adres.idAdresu)
            {
                return BadRequest();
            }

            db.Entry(adres).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresExists(id))
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

        // POST: api/Adresss
        [ResponseType(typeof(Adres))]
        public IHttpActionResult PostAdres(Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ksiazka_adresow.Add(adres);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adres.idAdresu }, adres);
        }

        // DELETE: api/Adresss/5
        [ResponseType(typeof(Adres))]
        public IHttpActionResult DeleteAdres(int id)
        {
            Adres adres = db.Ksiazka_adresow.Find(id);
            if (adres == null)
            {
                return NotFound();
            }

            db.Ksiazka_adresow.Remove(adres);
            db.SaveChanges();

            return Ok(adres);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdresExists(int id)
        {
            return db.Ksiazka_adresow.Count(e => e.idAdresu == id) > 0;
        }
    }
}