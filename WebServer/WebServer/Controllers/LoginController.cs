using System;
using System.Linq;

using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Classes;

using WebServer.Models;

namespace WebServer.Controllers
{
    public class LoginController : ApiController
    {
        private DB_A25DBA_magazynEntities db = new DB_A25DBA_magazynEntities();

        // POST: api/Login
        [HttpPost]
        [ActionName("Login")]
        [ResponseType(typeof(Pracownik))]
        public Pracownik LoginPracownik(LoginPassword loginPassword)
        {
            try
            {
                Pracownik pracownik = db.Pracownicy.FirstOrDefault(
                    x => x.Login == loginPassword.login && x.Haslo == loginPassword.password);

                return pracownik;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex} " + nameof(LoginPracownik));
                return null;
            }
        }
    }
}