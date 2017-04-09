using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class TransItemController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/TransItem
        [HttpGet]
        [ActionName("GetTransItems")]
        public IQueryable<Artykul_w_transakcji> GetTransItems()
        {
            return db.Artykuly_w_transakcji;
        }

        [HttpGet]
        [ActionName("GetTransItem")]
        [ResponseType(typeof(Artykul))]
        public IQueryable GetTransItem(int id)
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
    }
}