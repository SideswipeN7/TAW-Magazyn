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
        [ResponseType(typeof(int))]
        public int PostPracownik(LoginPassword id)
        {
            Pracownik pracownik = db.Pracownicy.FirstOrDefault(x => x.Login == id.login);
            if (pracownik == null)
            {
                return 0;
            }

            if (pracownik.Haslo == id.password)
            {
                return pracownik.idPracownika;
            }
            return 0;
        }
    }
}