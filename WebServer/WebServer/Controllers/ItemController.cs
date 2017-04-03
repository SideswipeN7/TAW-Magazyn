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
    public class ItemController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Item
        public IQueryable<Artykul> GetArtykuly()
        {
            return db.Artykuly;
        }

        // GET: api/Item/5
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult GetArtykul(int id)
        {
            Artykul artykul = db.Artykuly.Find(id);
            if (artykul == null)
            {
                return NotFound();
            }

            return Ok(artykul);
        }

        // PUT: api/Item/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtykul(int id, Artykul artykul)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artykul.idArtykulu)
            {
                return BadRequest();
            }

            db.Entry(artykul).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtykulExists(id))
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

        // POST: api/Item
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult PostArtykul(Artykul artykul)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artykuly.Add(artykul);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artykul.idArtykulu }, artykul);
        }

        // DELETE: api/Item/5
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult DeleteArtykul(int id)
        {
            Artykul artykul = db.Artykuly.Find(id);
            if (artykul == null)
            {
                return NotFound();
            }

            db.Artykuly.Remove(artykul);
            db.SaveChanges();

            return Ok(artykul);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtykulExists(int id)
        {
            return db.Artykuly.Count(e => e.idArtykulu == id) > 0;
        }
    }
}