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
    public class TransItemController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/TransItem
        public IQueryable<Artykul_w_transakcji> GetArtykuly_w_transakcji()
        {
            return db.Artykuly_w_transakcji;
        }


        [ResponseType(typeof(Artykul))]
        public IQueryable GetArtykuly_w_transakcji(int id)
        {

            var result = from a in db.Artykuly_w_transakcji
                         where a.idTransakcji == id
                         select new
                         {
                             idTransakcji = a.idTransakcji,
                             idArtykulu = a.Artykuly.idArtykulu,
                             Nazwa = a.Artykuly.Nazwa,
                             Ilosc = a.Artykuly.Ilosc,
                             Cena = a.Artykuly.Cena,
                             idKategorii = a.Artykuly.idKategorii
                         };
  
            return result;
        }


        [HttpPost]
        [ActionName("RegisterTransItems")]
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult RegisterTransItems(ICollection<Artykul_w_transakcji> kolekcja)
        {
            int dl = kolekcja.Count;
            int przetworzone = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (Artykul_w_transakcji a in kolekcja)
            {
                    try
                    {
                        db.Artykuly_w_transakcji.Add(a);
                        db.SaveChanges();
                        przetworzone++;

                    if (przetworzone == dl)
                        {
                            return StatusCode(HttpStatusCode.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(HttpStatusCode.Conflict);
                    }
    
            }
            return StatusCode(HttpStatusCode.Conflict);
        }


        // PUT: api/TransItem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtykul_w_transakcji(int id, Artykul_w_transakcji artykul_w_transakcji)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artykul_w_transakcji.idArt_w_trans)
            {
                return BadRequest();
            }

            db.Entry(artykul_w_transakcji).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Artykul_w_transakcjiExists(id))
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

        //// POST: api/TransItem
        //[ResponseType(typeof(Artykul_w_transakcji))]
        //public IHttpActionResult PostArtykul_w_transakcji(Artykul_w_transakcji artykul_w_transakcji)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Artykuly_w_transakcji.Add(artykul_w_transakcji);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = artykul_w_transakcji.idArt_w_trans }, artykul_w_transakcji);
        //}

        // DELETE: api/TransItem/5
        [ResponseType(typeof(Artykul_w_transakcji))]
        public IHttpActionResult DeleteArtykul_w_transakcji(int id)
        {
            Artykul_w_transakcji artykul_w_transakcji = db.Artykuly_w_transakcji.Find(id);
            if (artykul_w_transakcji == null)
            {
                return NotFound();
            }

            db.Artykuly_w_transakcji.Remove(artykul_w_transakcji);
            db.SaveChanges();

            return Ok(artykul_w_transakcji);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Artykul_w_transakcjiExists(int id)
        {
            return db.Artykuly_w_transakcji.Count(e => e.idArt_w_trans == id) > 0;
        }
    }
}