using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class AdresController : ApiController
    {
        private magazynEntities db = new magazynEntities();


        [HttpPost]
        [ActionName("RegisterAddress")]
        [ResponseType(typeof(Adres))]
        public int RegisterAddress(Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return 0;
            }

            Adres newAdres = db.Ksiazka_adresow.FirstOrDefault(a => a.idAdresu == adres.idAdresu);

            if (newAdres == null)
            {
                try
                {
                    db.Ksiazka_adresow.Add(adres);
                    db.SaveChanges();
                    return adres.idAdresu; // Yes it's here
                }
                catch (DbEntityValidationException ex)
                {
                    return 0;
                }
            }
            return newAdres.idAdresu;
        }
        // GET: api/Adres
        public IQueryable<Adres> GetKsiazka_adresow()
        {
            return db.Ksiazka_adresow;
        }

        // GET: api/Adres/5
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

        // PUT: api/Adres/5
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

        // POST: api/Adres
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

        // DELETE: api/Adres/5
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