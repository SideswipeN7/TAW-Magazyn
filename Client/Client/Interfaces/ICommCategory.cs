using Client.Model;
using System.Collections.Generic;

namespace Client.Interfaces
{
    public interface ICommCategory
    {
        bool RegisterCategory(Kategoria kategoria);

        IEnumerable<Kategoria> GetCategories();

        bool ChangeCategory(Kategoria kategoria);

        void DeleteCategory(Kategoria kategoria);
    }
}