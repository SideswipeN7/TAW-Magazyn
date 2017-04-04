<<<<<<< HEAD
﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
=======
﻿using System.Linq;
>>>>>>> BranchStana
using System.Web.Http;
using System.Web.Http.Description;
using WebServer.Classes;
<<<<<<< HEAD
=======
using WebServer.Models;
>>>>>>> BranchStana

namespace WebServer.Controllers
{
    public class LoginController : ApiController
    {
        private magazynEntities db = new magazynEntities();

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