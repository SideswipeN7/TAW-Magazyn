using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDataLib
{
    public class LoginData
    {
        public int Sudo { get; set; }
        public int Id { get; set; }

        public LoginData(int Id, int Sudo)
        {
            this.Sudo = Sudo;
            this.Id = Id;
        }
    }
}
