using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    interface ICommCategory
    {
        bool RegisterCategory(Kategoria kategoria);

        IEnumerable<Kategoria> GetCategories();

        bool ChangeCategory(Kategoria kategoria);
        void DeleteCategory(Kategoria kategoria);
    }
}
