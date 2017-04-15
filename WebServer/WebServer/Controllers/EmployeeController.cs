using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class EmployeeController : ApiController
    {
        private DB_A1D841_magazynEntities db = new DB_A1D841_magazynEntities();

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
                db.Pracownicy.Add(pracownik);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }
    }
}