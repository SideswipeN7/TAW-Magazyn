using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    interface ICommItems
    {
        IEnumerable<Artykul> GetItems();
        bool ChangeItem(Artykul artykul);
        bool RegisterItem(Artykul artykul);
        void DeleteItem(int idArtykulu);
    }
}
