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
    public class TransactionController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        //    // GET: api/Transaction
        //    public IQueryable<Transakcja> GetTransakcje()
        //    {
        //        return db.Transakcje;
        //    }

        //    // GET: api/Transaction/5
        //    [ResponseType(typeof(Transakcja))]
        //    public IHttpActionResult GetTransakcja(int id)
        //    {
        //        Transakcja transakcja = db.Transakcje.Find(id);
        //        if (transakcja == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(transakcja);
        //    }

        //    // PUT: api/Transaction/5
        //    [ResponseType(typeof(void))]
        //    public IHttpActionResult PutTransakcja(int id, Transakcja transakcja)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (id != transakcja.idTransakcji)
        //        {
        //            return BadRequest();
        //        }

        //        db.Entry(transakcja).State = EntityState.Modified;

        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TransakcjaExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return StatusCode(HttpStatusCode.NoContent);
        //    }

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
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return Content(HttpStatusCode.Conflict, transakcja);
            }
        }

        //    // DELETE: api/Transaction/5
        //    [ResponseType(typeof(Transakcja))]
        //    public IHttpActionResult DeleteTransakcja(int id)
        //    {
        //        Transakcja transakcja = db.Transakcje.Find(id);
        //        if (transakcja == null)
        //        {
        //            return NotFound();
        //        }

        //        db.Transakcje.Remove(transakcja);
        //        db.SaveChanges();

        //        return Ok(transakcja);
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }

        //    private bool TransakcjaExists(int id)
        //    {
        //        return db.Transakcje.Count(e => e.idTransakcji == id) > 0;
        //    }
    }
}