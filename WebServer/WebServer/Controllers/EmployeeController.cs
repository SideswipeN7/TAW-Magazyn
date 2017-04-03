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
    public class EmployeeController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Employee
        public IQueryable<Pracownik> GetPracownicy()
        {
            return db.Pracownicy;
        }

        // GET: api/Employee/5
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

        // PUT: api/Employee/5
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

        // POST: api/Employee
        [ResponseType(typeof(Pracownik))]
        public IHttpActionResult PostPracownik(Pracownik pracownik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pracownicy.Add(pracownik);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pracownik.idPracownika }, pracownik);
        }

        // DELETE: api/Employee/5
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