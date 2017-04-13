using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginExecutor
{
    public interface IPluginLogin
    {
        string Login(string login, string password);
    }
}
