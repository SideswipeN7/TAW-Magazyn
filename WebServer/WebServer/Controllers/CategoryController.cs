using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class CategoryController : ApiController
    {
        private magazynEntities db = new magazynEntities();

        // GET: api/Category
        [HttpGet]
        [ActionName("GetCategories")]
        public IQueryable<Kategoria> GetCategories()
        {
            return db.Kategorie;
        }

        // PUT: api/Category/5
        [HttpPut]
        [ActionName("ChangeCategory")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeCategory(Kategoria kategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(kategoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Error: {ex}");
                return Content(HttpStatusCode.Conflict, kategoria);
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Category
        [HttpPost]
        [ActionName("RegisterCategory")]
        [ResponseType(typeof(Kategoria))]
        public IHttpActionResult RegisterCategory(Kategoria kategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Kategorie.Add(kategoria);
                db.SaveChanges();
                int id = kategoria.idKategorii;

                return Content(HttpStatusCode.Created, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return Content(HttpStatusCode.Conflict, kategoria);
            }
        }
    }
}