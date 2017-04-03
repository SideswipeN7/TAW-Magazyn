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
    public class CategoryController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Category
        public IQueryable<Kategoria> GetCategories()
        {
            return db.Kategorie;
        }

        // GET: api/Category/5
        [ResponseType(typeof(Kategoria))]
        public IHttpActionResult GetKategoria(int id)
        {
            Kategoria kategoria = db.Kategorie.Find(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return Ok(kategoria);
        }

        // PUT: api/Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKategoria(int id, Kategoria kategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kategoria.idKategorii)
            {
                return BadRequest();
            }

            db.Entry(kategoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategoriaExists(id))
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

        // POST: api/Category
        [ResponseType(typeof(Kategoria))]
        public IHttpActionResult PostKategoria(Kategoria kategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kategorie.Add(kategoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kategoria.idKategorii }, kategoria);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(Kategoria))]
        public IHttpActionResult DeleteKategoria(int id)
        {
            Kategoria kategoria = db.Kategorie.Find(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            db.Kategorie.Remove(kategoria);
            db.SaveChanges();

            return Ok(kategoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KategoriaExists(int id)
        {
            return db.Kategorie.Count(e => e.idKategorii == id) > 0;
        }
    }
}