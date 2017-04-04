using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class TransactionController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Transaction
        [HttpGet]
        [ActionName("GetTransactions")]
        public IQueryable<Transakcja> GetTransactions()
        {
            return db.Transakcje;
        }

        // GET: api/Transaction/5
        [HttpGet]
        [ActionName("GetTransaction ")]
        [ResponseType(typeof(Transakcja))]
        public IHttpActionResult GetTransaction(int id)
        {
            Transakcja transakcja = db.Transakcje.Find(id);
            if (transakcja == null)
            {
                return NotFound();
            }

            return Ok(transakcja);
        }

        // POST: api/Transaction
        [HttpPost]
        [ActionName("RegisterTransaction ")]
        [ResponseType(typeof(Transakcja))]
        public IHttpActionResult RegisterTransaction(Transakcja transakcja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Transakcje.Add(transakcja);
                db.SaveChanges();
                int id = transakcja.idTransakcji;

                return Content(HttpStatusCode.Created, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return Content(HttpStatusCode.Conflict, transakcja);
            }
        }

    }
}