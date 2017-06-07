using Client.Model;
using System.Collections.Generic;

namespace Client.Interfaces
{
    public interface ICommItems
    {
        IEnumerable<Artykul> GetItems();

        bool ChangeItem(Artykul artykul);

        bool RegisterItem(Artykul artykul);

        void DeleteItem(int idArtykulu);
    }
}