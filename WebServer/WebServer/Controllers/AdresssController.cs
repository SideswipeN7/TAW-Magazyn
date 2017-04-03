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
        public Adres GetAddress(int id)
        {
            Adres adres = db.Ksiazka_adresow.Find(id);
            return adres;
        }

        // PUT: api/Adresss/5
        [HttpPut]
        [ActionName("ChangeAddress")]
        [ResponseType(typeof(void))]
        public bool ChangeAdres(Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var oldAdress = db.Ksiazka_adresow.SingleOrDefault(b => b.idAdresu == adres.idAdresu);
            if (oldAdress != null)
            {
                try
                {
                    db.Entry(adres).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        // POST: api/Adresss
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