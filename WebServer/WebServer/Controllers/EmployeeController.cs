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
    }
}