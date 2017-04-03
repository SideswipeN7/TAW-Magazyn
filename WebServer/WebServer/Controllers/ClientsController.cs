using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class ClientsController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Clients
        public IQueryable<Klient> GetKlienci()
        {
            return db.Klienci;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Klient))]
        public IHttpActionResult GetKlient(int id)
        {
            Klient klient = db.Klienci.Find(id);
            if (klient == null)
            {
                return NotFound();
            }

            return Ok(klient);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKlient(int id, Klient klient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != klient.idKlienta)
            {
                return BadRequest();
            }

            db.Entry(klient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Klient))]
        public IHttpActionResult PostKlient(Klient klient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Klienci.Add(klient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = klient.idKlienta }, klient);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Klient))]
        public IHttpActionResult DeleteKlient(int id)
        {
            Klient klient = db.Klienci.Find(id);
            if (klient == null)
            {
                return NotFound();
            }

            db.Klienci.Remove(klient);
            db.SaveChanges();

            return Ok(klient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KlientExists(int id)
        {
            return db.Klienci.Count(e => e.idKlienta == id) > 0;
        }
    }
}