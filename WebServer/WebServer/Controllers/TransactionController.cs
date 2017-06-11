using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class TransactionController : ApiController
    {
        private DB_A25DBA_magazynEntities db = new DB_A25DBA_magazynEntities();

        // GET: api/Transaction
        [HttpGet]
        [ActionName("GetTransactions")]
        public IQueryable<Transakcja> GetTransactions()
        {
            IQueryable<Transakcja> transactions = db.Transakcje;
            foreach(Transakcja t in transactions)
            {
                t.Pracownicy = db.Pracownicy.FirstOrDefault(x => x.idPracownika == t.idPracownika);
                t.Klienci = db.Klienci.FirstOrDefault(x => x.idKlienta == t.idKlienta);
                IEnumerable<Artykul_w_transakcji> art = db.Artykuly_w_transakcji.Where(x => x.idTransakcji == t.idTransakcji).ToArray();
                foreach(Artykul_w_transakcji a in art)
                {
                    a.Artykuly = db.Artykuly.FirstOrDefault(x => x.idArtykulu == a.idArtykulu);
                }
                t.Dostawcy = db.Dostawcy.FirstOrDefault(x => x.idDostawcy == t.idDostawcy);
                t.Klienci.Ksiazka_adresow = db.Ksiazka_adresow.FirstOrDefault(x => x.idAdresu == t.Klienci.idAdresu);
            }
            return transactions;
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
            else
            {

                transakcja.Pracownicy = db.Pracownicy.FirstOrDefault(x => x.idPracownika == transakcja.idPracownika);
                transakcja.Klienci = db.Klienci.FirstOrDefault(x => x.idKlienta == transakcja.idKlienta);
                    IEnumerable<Artykul_w_transakcji> art = db.Artykuly_w_transakcji.Where(x => x.idTransakcji == transakcja.idTransakcji).ToArray();
                    foreach (Artykul_w_transakcji a in art)
                    {
                        a.Artykuly = db.Artykuly.FirstOrDefault(x => x.idArtykulu == a.idArtykulu);
                    }
                
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
                Adres a = transakcja.Klienci.Ksiazka_adresow;
                Klient k = transakcja.Klienci;
                new ClientsController().RegisterClient(new KlientAdress() { Adres = a, Klient = k });
                transakcja.idKlienta = k.idKlienta;
                db.Transakcje.Add(transakcja);
                db.SaveChanges();
                int id = transakcja.idTransakcji;
                foreach(Artykul_w_transakcji at in transakcja.Artykuly_w_transakcji)
                {
                    Artykul ar = db.Artykuly.Find(at.idArtykulu);
                    ar.Ilosc -= 1;
                    db.Entry(ar).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return Content(HttpStatusCode.Created, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex} " + nameof(RegisterTransaction));
                return Content(HttpStatusCode.Conflict, transakcja);
            }
        }
    }
}