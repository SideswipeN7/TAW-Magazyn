using System.Linq;

using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Classes;

using WebServer.Models;

namespace WebServer.Controllers
{
    public class LoginController : ApiController
    {
        private DB_A1D841_magazynEntities1 db = new DB_A1D841_magazynEntities1();

        // POST: api/Login
        [HttpPost]
        [ActionName("Login")]
        [ResponseType(typeof(Pracownik))]
        public Pracownik LoginPracownik(LoginPassword loginPassword)
        {
            Pracownik pracownik = db.Pracownicy.FirstOrDefault(
                x => x.Login == loginPassword.login && x.Haslo == loginPassword.password);

            return pracownik;
        }
    }
}