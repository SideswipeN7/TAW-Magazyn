using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class EmployeeController : ApiController
    {
        private DB_A1D841_magazynEntities1 db = new DB_A1D841_magazynEntities1();

        // POST: api/Employee/Register
        //[HttpPost]

        [HttpGet]
        [ActionName("GetEmployees")]
        public IQueryable<Pracownik> GetKPracownicy()
        {
            try
            {
                IQueryable<Pracownik> employees = db.Pracownicy;
                foreach (Pracownik p in employees)
                {
                    p.Ksiazka_adresow = db.Ksiazka_adresow.FirstOrDefault(x => x.idAdresu == p.idAdresu);
                }
                return employees;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\nERROR: {ex}");
                return null;
            }
        }

        [HttpGet]
        [ActionName("RegisterEmployee")]
        public HttpResponseMessage RegisterEmployee(Pracownik pracownik, Adres adres)
        {
            try
            {
                db.Ksiazka_adresow.Add(adres);
                adres.idAdresu = db.Ksiazka_adresow.Find(adres).idAdresu;
                pracownik.idAdresu = adres.idAdresu;
                pracownik.Ksiazka_adresow = adres;
                db.Pracownicy.Add(pracownik);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }
        [HttpDelete]
        [ActionName("DeleteEmployee")]
        [ResponseType(typeof(Pracownik))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Pracownik pracownik = db.Pracownicy.Find(id);
            if (pracownik == null)
            {
                return NotFound();
            }

            db.Pracownicy.Remove(pracownik);
            db.SaveChanges();

            return Ok(pracownik);
        }

        [HttpPut]
        [ActionName("ModifyEmployee")]
        [ResponseType(typeof(void))]
        public bool ModifyEmployee(Pracownik pracownik, Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {
                db.Entry(adres).State = EntityState.Modified;
                db.Entry(pracownik).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}