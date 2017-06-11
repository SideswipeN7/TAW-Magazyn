using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class SupplyController : ApiController
    {
        private DB_A25DBA_magazynEntities db = new DB_A25DBA_magazynEntities();

        // GET: api/Supply
        [HttpGet]
        [ActionName("GetSuppliers")]
        public IQueryable<Dostawca> GetSuppliers()
        {
            return db.Dostawcy;
        }

        // GET: api/Supply/5
        [HttpGet]
        [ActionName("GetSupplier")]
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
        [HttpPut]
        [ActionName("ChangeSupplier")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeSupplier(Dostawca supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex} " + nameof(ChangeSupplier));
                return StatusCode(HttpStatusCode.NotModified);
            }
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
            if (newSupplier == null)
            {
                try
                {
                    db.Dostawcy.Add(supplier);
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR: {ex} " + nameof(RegisterSupplier));
                    return StatusCode(HttpStatusCode.Conflict);
                }
            }
            return StatusCode(HttpStatusCode.Conflict);
        }

        [HttpDelete]
        [ActionName("DeleteSupply")]
        [ResponseType(typeof(Artykul))]
        public IHttpActionResult DeleteSupply(int id)
        {
            Dostawca dostawva = db.Dostawcy.Find(id);
            if (dostawva == null)
            {
                return NotFound();
            }

            db.Dostawcy.Remove(dostawva);
            db.SaveChanges();

            return Ok(dostawva);
        }
    }
}