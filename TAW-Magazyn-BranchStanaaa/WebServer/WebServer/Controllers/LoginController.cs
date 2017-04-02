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
using WebServer.Classes;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebServer.Controllers
{
    public class LoginController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Login
        public IQueryable<Pracownik> GetPracownicy()
        {
            return db.Pracownicy;
        }

        // GET: api/Login/5
        [ResponseType(typeof(Pracownik))]
        public IHttpActionResult GetPracownik(int id)
        {
            Pracownik pracownik = db.Pracownicy.Find(id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return Ok(pracownik);
        }

        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPracownik(int id, Pracownik pracownik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pracownik.idPracownika)
            {
                return BadRequest();
            }

            db.Entry(pracownik).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PracownikExists(id))
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

        // POST: api/Login
        //[ResponseType(typeof(Pracownik))]
        public int PostPracownik(LoginPassword id)
        {
            Pracownik pracownik = db.Pracownicy.FirstOrDefault(x => x.Login == id.login);
            if (pracownik == null)
            {
                return 0;
            }

            if (pracownik.Haslo == id.password)
            {
                return pracownik.idPracownika;
            }
            return 0;

        }

        // DELETE: api/Login/5
        [ResponseType(typeof(Pracownik))]
        public IHttpActionResult DeletePracownik(int id)
        {
            Pracownik pracownik = db.Pracownicy.Find(id);
            if (pracownik == null)
            {
                return NotFound();
            }

            db.Pracownicy.Remove(pracownik);
            db.SaveChanges();

            return Ok(pracownik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PracownikExists(int id)
        {
            return db.Pracownicy.Count(e => e.idPracownika == id) > 0;
        }
        
    }
}