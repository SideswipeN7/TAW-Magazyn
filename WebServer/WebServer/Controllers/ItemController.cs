using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class ItemController : ApiController
    {
        private DB_A1D841_magazynEntities1 db = new DB_A1D841_magazynEntities1();

        // GET: api/Item
        [HttpGet]
        [ActionName("GetItems")]
        public IQueryable<Artykul> GetItems()
        {
            IQueryable<Artykul> art= db.Artykuly;
            foreach(Artykul a in art)
            {
                a.Kategorie = db.Kategorie.FirstOrDefault(x => x.idKategorii == a.idKategorii);
            }
            return art;
        }

        // PUT: api/Item/5
        [HttpPut]
        [ActionName("ChangeItem")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeItem(Artykul artykul)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Entry(artykul).State = EntityState.Modified;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }

        // POST: api/Item
        [HttpPost]
        [ActionName("RegisterItem")]
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult RegisterItem(Artykul art)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Artykul newArt = db.Artykuly.FirstOrDefault(a => a.idArtykulu == art.idArtykulu);

            if (newArt == null)
            {
                try
                {
                    db.Artykuly.Add(art);
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.Created);
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