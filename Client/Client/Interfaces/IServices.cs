namespace Client.Adapter
{
    internal interface IAdapter
    {
        //Categories
        void ShowAllCategories();

        void ShowSearchCategories();

        void ShowAddCategories();

        void ShowDeleteCategories();

        void ShowModifyCategories();

        void AddCategories();

        void DeleteCategories();

        void ModifyCategories();

        void SearchCategories();

        void SelectedCategories();

        //Items
        void ShowAllItems();

        void ShowSearchItems();

        void ShowAddItems();

        void ShowDeleteItems();

        void ShowModifyItems();

        void AddItems();

        void DeleteItems();

        void ModifyItems();

        void SearchItems();

        void SelectedItems();

        //Clients
        void ShowAllClients();

        void ShowSearchClients();

        void ShowAddClients();

        void ShowDeleteClients();

        void ShowModifyClients();

        void AddClients();

        void DeleteClients();

        void ModifyClients();

        void SearchClients();

        void SelectedClients();

        //Employee
        void ShowAllEmployee();

        void ShowSearchEmployee();

        void ShowAddEmployee();

        void ShowDeleteEmployee();

        void ShowModifyEmployee();

        void AddEmployee();

        void DeleteEmployee();

        void ModifyEmployee();

        void SearchEmployee();

        void SelectedEmployee();

        //Transaction
        void ShowAllTransaction();

        void ShowSearchTransaction();

        void ShowAddTransaction();

        void ShowDeleteTransaction();

        void ShowModifyTransaction();

        void AddTransaction();

        void DeleteTransaction();

        void ModifyTransaction();

        void SearchTransaction();

        void SelectedTransaction();

        //Magazine
        void ShowAllMagazine();

        void ShowSearchMagazine();

        void AddMagazine();

        void DeleteMagazine();

        void ModifyMagazine();

        void SearchMagazine();

        void SelectedMagazine();

        //Other
        void ShowPassword();

        void TransactionNewSelcted();

        void TransactionOldSelcted();

        void GetFacture();

        void SelectClientDoTransactionSurname();

        void SelectClientDoTransactionFirm();

        void AddToCart();

        void DeleteFormCart();

        void CmbCategoryIdChange();

        void GetAll();

        void LoadAll();

        void SelectaAll();
    }
}