using System;
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
    public class ClientsController : ApiController
    {
        private DB_A25DBA_magazynEntities db = new DB_A25DBA_magazynEntities();

        // GET: api/Clients
        [HttpGet]
        [ActionName("GetClients")]
        public IQueryable<Klient> GetKlienci()
        {
            try
            {
                IQueryable<Klient> clients = db.Klienci;
                foreach (Klient k in clients)
                {
                    k.Ksiazka_adresow = db.Ksiazka_adresow.FirstOrDefault(x => x.idAdresu == k.idAdresu);
                    k.Transakcje = db.Transakcje.Where(x => x.idKlienta == k.idKlienta).ToArray();
                }
                return clients;
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\nERROR: {ex}");
                return null;
            }
        }

        // GET: api/Clients/5
        [HttpGet]
        [ActionName("GetClient")]
        [ResponseType(typeof(Klient))]
        public Klient GetClient(int id)
        {
            Klient klient = db.Klienci.Find(id);
            if (klient == null)
            {
                return null;
            }
            else
            {
                klient.Ksiazka_adresow = db.Ksiazka_adresow.FirstOrDefault(x => x.idAdresu == klient.idAdresu);
                klient.Transakcje = db.Transakcje.Where(x => x.idKlienta == klient.idKlienta).ToArray();
            }

            return klient;
        }

        // POST: api/Clients/Register
        [HttpPost]
        [ActionName("RegisterClient")]
        public HttpResponseMessage RegisterClient(KlientAdress dane)
        {
            try
            {
                var result = new AdresssController().RegisterAddress(dane.Adres);
                //db.Ksiazka_adresow.Add(adres);
                //adres.idAdresu = db.Ksiazka_adresow.Find(adres).idAdresu;
                //klient.idAdresu = adres.idAdresu;
                dane.Klient.idAdresu = result;
                db.Klienci.Add(dane.Klient);                                
                db.SaveChanges();
                dane.Klient.idKlienta = db.Klienci.FirstOrDefault(x =>
                x.idAdresu == dane.Klient.idAdresu &&
                x.Imie.Equals(dane.Klient.Imie) &&
                x.Nazwisko.Equals(dane.Klient.Nazwisko) &&
                x.Nazwa_firmy.Equals(dane.Klient.Nazwisko)
                ).idKlienta;
                return Request.CreateResponse(HttpStatusCode.Created, dane.Klient.idKlienta);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{Environment.NewLine}Exception in RegisterClient{Environment.NewLine}{ex}{Environment.NewLine}");
                return Request.CreateResponse(HttpStatusCode.Conflict,0);
            }
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [ActionName("ChangeClient")]
        public HttpResponseMessage ChangeClient(Klient klient)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            db.Entry(klient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [ActionName("DeleteClient")]
        [ResponseType(typeof(HttpStatusCode))]
        public HttpStatusCode DeleteClient(Klient klient)
        {
            try
            {
                db.Klienci.Remove(klient);
                db.SaveChanges();
                return HttpStatusCode.Gone;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex}");
                return HttpStatusCode.NotModified;
            }
        }

        [HttpDelete]
        [ActionName("DeleteClient")]
        [ResponseType(typeof(Klient))]
        public IHttpActionResult DeleteClient(int id)
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
    }
}