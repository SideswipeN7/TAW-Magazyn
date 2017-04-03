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
        [HttpPut]
        [ActionName("ChangeItem")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeItem(Artykul artykul)
        {
            var oldArt = db.Artykuly.SingleOrDefault(b => b.idArtykulu == artykul.idArtykulu);
            if (oldArt != null)
            {
                oldArt.Nazwa = artykul.Nazwa;
                oldArt.Ilosc = artykul.Ilosc;
                oldArt.Cena = artykul.Cena;
                oldArt.idKategorii = artykul.idKategorii;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }

            return StatusCode(HttpStatusCode.Conflict);
        }

        // POST: api/Item
        [HttpPost]
        [ActionName("RegisterItem")]
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult RegisterItem(Artykul art)
        {
            Artykul newArt = db.Artykuly.FirstOrDefault(a => a.idArtykulu == art.idArtykulu);
            
            if (newArt == null)
            {
                db.Artykuly.Add(art);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }
            return StatusCode(HttpStatusCode.Conflict);
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