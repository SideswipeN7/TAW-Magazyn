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
    public class SupplyController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Supply
        public IQueryable<Dostawca> GetSuppliers()
        {
            return db.Dostawcy;
        }

        // GET: api/Supply/5
        [ResponseType(typeof(Dostawca))]
        public IHttpActionResult GetSupplier(int id)
        {
            Dostawca dostawca = db.Dostawcy.Find(id);
            if (dostawca == null)
            {
                return NotFound();
            }

            return Ok(dostawca);
        }

        // PUT: api/Supply/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDostawca(int id, Dostawca dostawca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dostawca.idDostawcy)
            {
                return BadRequest();
            }

            db.Entry(dostawca).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DostawcaExists(id))
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

        // POST: api/Supply
        [HttpPost]
        [ActionName("RegisterSupplier")]
        [ResponseType(typeof(Dostawca))]
        public IHttpActionResult RegisterSupplier(Dostawca supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dostawca newSupplier = db.Dostawcy.FirstOrDefault(a => a.idDostawcy == supplier.idDostawcy);
            if(newSupplier == null)
            {
                try
                {
                    db.Dostawcy.Add(newSupplier);
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

        // DELETE: api/Supply/5
        [ResponseType(typeof(Dostawca))]
        public IHttpActionResult DeleteDostawca(int id)
        {
            Dostawca dostawca = db.Dostawcy.Find(id);
            if (dostawca == null)
            {
                return NotFound();
            }

            db.Dostawcy.Remove(dostawca);
            db.SaveChanges();

            return Ok(dostawca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DostawcaExists(int id)
        {
            return db.Dostawcy.Count(e => e.idDostawcy == id) > 0;
        }
    }
}