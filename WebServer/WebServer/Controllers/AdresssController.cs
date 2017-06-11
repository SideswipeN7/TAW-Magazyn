using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class AdresssController : ApiController
    {
        private DB_A25DBA_magazynEntities db = new DB_A25DBA_magazynEntities();

        // GET: api/Adresss
        public IQueryable<Adres> GetKsiazka_adresow()
        {
            return db.Ksiazka_adresow;
        }

        // GET: api/Adresss/5
        [ResponseType(typeof(Adres))]
        public Adres GetAddress(int id)
        {
            Adres adres = db.Ksiazka_adresow.Find(id);
            return adres;
        }

        // PUT: api/Adresss/5
        [HttpPut]
        [ActionName("ChangeAddress")]
        [ResponseType(typeof(void))]
        public bool ChangeAdres(Adres adres)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {
                db.Entry(adres).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex} " + nameof(ChangeAdres));
                return false;
            }
        }

        // POST: api/Adresss
        [HttpPost]
        [ActionName("RegisterAddress")]
        [ResponseType(typeof(int))]
        public int RegisterAddress(Adres adres)
        {
            (!ModelState.IsValid) ?? 0;            

            Adres newAdres = db?.Ksiazka_adresow?.FirstOrDefault(a => a.idAdresu == adres.idAdresu);            

            if (newAdres == null)
            {
                try
                {
                    db.Ksiazka_adresow.Add(adres);
                    db.SaveChanges();
                    return adres.idAdresu;
                }
                catch (DbEntityValidationException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR: {ex} " + nameof(RegisterAddress));
                    return 0;
                }
            }
            return newAdres.idAdresu;
        }
    }
}