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
        private DB_A1D841_magazynEntities1 db = new DB_A1D841_magazynEntities1();

        // GET: api/Clients
        [HttpGet]
        [ActionName("GetClients")]
        public IQueryable<Klient> GetKlienci()
        {
            IQueryable<Klient> clients = db.Klienci;
            foreach(Klient k in clients)
            {
                k.Ksiazka_adresow = db.Ksiazka_adresow.FirstOrDefault(x => x.idAdresu == k.idAdresu);
                k.Transakcje = db.Transakcje.Where(x => x.idKlienta == k.idKlienta).ToArray();
            }
            return clients;
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
        public HttpResponseMessage RegisterClient(Klient klient, Adres adres)
        {
            try
            {
                var result = new AdresssController().RegisterAddress(adres);
                //db.Ksiazka_adresow.Add(adres);
                //adres.idAdresu = db.Ksiazka_adresow.Find(adres).idAdresu;
                //klient.idAdresu = adres.idAdresu;
                klient.idAdresu = result;
                db.Klienci.Add(klient);
                db.SaveChanges();                
                return Request.CreateResponse(HttpStatusCode.Created,klient.idKlienta);
            }
            catch
            {
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
            }catch(Exception ex)
            {
                return HttpStatusCode.NotModified;
            }
        }
    }
}