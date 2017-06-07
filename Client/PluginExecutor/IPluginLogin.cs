using System.Collections.Generic;

namespace PluginExecutor
{
    public interface IPluginLogin
    {
        string Execute(Dictionary<int,string> dictionary);
    }
}