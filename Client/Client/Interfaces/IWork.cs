using System.Collections.Generic;

namespace Client.Controller
{
    internal interface IWork
    {
        void ChangeData();

        void DeleteData();

        void ShowData();

        void ShowSelectedData();

        void AddData();

        IEnumerable<object> GetData();

        void SearchData();
    }
}