using Client.Interfaces;
using Client.Model;
using Client.Windows;
using Client.Controller;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace TDD_TEST
{
    [TestFixture]
    public class UnitTest1
    {

        Mock<ICommCategory> mockICommCategory;
        Mock<ICommClient> mockICommClient;
        Mock<ICommEmployee> mockICommEmployee;
        Mock<ICommItems> mockICommItems;
        Mock<ICommSupplier> mockICommSupplier;
        Mock<ICommTransaction> mockICommTransaction;

        CategoryController category;
        ClientsController client;
        EmployeeController employee;
        ItemsController item;
        MagazineController magazine;
        TransactionController transaction;

        [SetUp]
        public void Setup()
        {
            mockICommCategory = new Mock<ICommCategory>();
            mockICommClient = new Mock<ICommClient>();
            mockICommEmployee = new Mock<ICommEmployee>();
            mockICommItems = new Mock<ICommItems>();
            mockICommSupplier = new Mock<ICommSupplier>();
            mockICommTransaction = new Mock<ICommTransaction>();
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void GetCategories()
        {
            //Arrange
            mockICommCategory.Setup(x => x.GetCategories()).Returns(TestCategory);
            category = CategoryController.GetInstance(mockICommCategory.Object);           

            //Act
            var res = category.GetData();

            //Assert
            res.Should().Equals(TestCategory);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void GetClients()
        {
            //Arrange
            mockICommClient.Setup(x => x.GetClients()).Returns(TestClient);
            client = ClientsController.GetInstance(mockICommClient.Object);

            //Act
            var res = client.GetData();

            //Assert
            res.Should().Equals(TestClient);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void GetEmployees()
        {
            //Arrange
            mockICommEmployee.Setup(x => x.GetEmpoyees()).Returns(TestEmployee);
            employee = EmployeeController.GetInstance(mockICommEmployee.Object);

            //Act
            var res = employee.GetData();

            //Assert
            res.Should().Equals(TestEmployee);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void GetItems()
        {
            //Arrange
            mockICommItems.Setup(x => x.GetItems()).Returns(TestItem);
            item = ItemsController.GetInstance(mockICommItems.Object);

            //Act
            var res = item.GetData();

            //Assert
            res.Should().Equals(TestItem);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void GetMagazine()
        {
            //Arrange
            mockICommItems.Setup(x => x.GetItems()).Returns(TestItem);
            magazine = MagazineController.GetInstance(mockICommItems.Object);

            //Act
            var res = magazine.GetData();

            //Assert
            res.Should().Equals(TestItem);
        }

        [Test]
        [Parallelizable(ParallelScope.All)]
        public void GetTransactions()
        {
            //Arrange
            mockICommTransaction.Setup(x => x.GetTransactions()).Returns(TestTransaction);
            transaction = TransactionController.GetInstance(mockICommTransaction.Object);

            //Act
            var res = transaction.GetData();

            //Assert
            res.Should().Equals(TestTransaction);
        }



        //[Test]
        //public void GetItems()
        //{
        //    List<Artykul> result = comm.GetItems().ToList();
        //    result.Count().Should().Equals(6);
        //}

        //[Test]
        //public void GetTransItems()
        //{
        //    List<Artykul_w_transakcji> result = comm.GetTransItems().ToList();
        //    result.Count().Should().Equals(6);
        //}

        //[Test]
        //public void GetSuppliers()
        //{
        //    IEnumerable<Dostawca> result = comm.GetSuppliers();
        //    result.Should().HaveCount(x => x > 5);
        //}

        //[Test]
        //public void ChangeItems()
        //{
        //    bool result = comm.ChangeItem(new Artykul { idArtykulu = 999, Nazwa = "LG OLED55B6J", Ilosc = 35, idKategorii = 5, Cena = 11799M });
        //    result.Should().BeFalse();
        //}

        //[Test]
        //[TestCase(0)]
        //[TestCase(1)]
        //[TestCase(null)]
        //public void GetAddress(int id)
        //{
        //    Adres result = comm.GetAddress(id);

        //    if (id == 1)
        //    {
        //        result.ShouldBeEquivalentTo(new Adres
        //        {
        //            idAdresu = 1,
        //            Miejscowosc = "Czeladz",
        //            Kod_pocztowy = "41-250",
        //            Wojewodztwo = "Slaskie"
        //        });
        //    }

        //    if (id == 0)
        //    {
        //        result.ShouldBeEquivalentTo(null);
        //    }
        //}

        //[Test]
        //[TestCase(2)]
        //[TestCase(null)]
        //public void GetTransaction(int id)
        //{
        //    Transakcja result = comm.GetTransaction(id);

        //    if (id == 2)
        //    {
        //        result.Should().Equals(new Transakcja
        //        {
        //            idTransakcji = 2,
        //            Data = new System.DateTime(2017, 06, 01, 13, 45, 30),
        //            idKlienta = 1,
        //            idPracownika = 3,
        //            idDostawcy = 1
        //        });
        //    }

        //    if (id == 0)
        //    {
        //        result.Should().Equals(null);
        //    }
        //}

        //[Test]
        //[TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeAddressCases))]
        //public void ChangeAddress(Adres adres)
        //{
        //    bool result = comm.ChangeAddress(adres);

        //    if (adres.idAdresu == 1)

        //    {
        //        result.Should().BeTrue();
        //    }
        //    if (adres.idAdresu == 999)
        //    {
        //        result.Should().BeFalse();
        //    }
        //}

        //[Test]
        //[TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestRegisterSupplyCases))]
        //public void RegisterSupply(Dostawca dostawca)
        //{
        //    bool result = comm.RegisterSupplier(dostawca);

        //    if (dostawca.idDostawcy == 1)
        //    {
        //        result.ShouldBeEquivalentTo(false);
        //    }

        //    if (dostawca.idDostawcy == 999)
        //    {
        //        result.ShouldBeEquivalentTo(true);
        //    }
        //}

        //[Test]
        //[TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeAddressCases))]
        //public void RegisterAddress(Adres adres)
        //{
        //    int result = comm.RegisterAddress(adres);

        //    if (adres.idAdresu == 1)
        //    {
        //        result.ShouldBeEquivalentTo(1);
        //    }
        //    else result.Should().BeGreaterThan(0);
        //}

        //[Test]
        //[TestCase(0)]
        //[TestCase(1)]
        //[TestCase(null)]
        //public void GetSupplier(int id)
        //{
        //    Dostawca result = comm.GetSupplier(id);

        //    if (id == 1)
        //    {
        //        result.ShouldBeEquivalentTo(new Dostawca
        //        {
        //            idDostawcy = 1,
        //            Nazwa = "Kurierex"
        //        });
        //    }

        //    if (id == 0)
        //    {
        //        result.ShouldBeEquivalentTo(null);
        //    }
        //}

        //[Test]
        //[TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestRegisterTransactionCases))]
        //public void RegisterTransaction(Transakcja transakcja)
        //{
        //    int result = comm.RegisterTransaction(transakcja);

        //    if (transakcja.idTransakcji == 1)
        //    {
        //        result.ShouldBeEquivalentTo(1);
        //    }
        //    else result.Should().BeGreaterThan(0);
        //}

        //[Test]
        //[TestCase("a", "b")]
        //[TestCase("RogalDDL", "P@ssw0rd")]
        //public void Login(string login, string haslo)
        //{
        //    //IPluginLogin i = new PluginLogin();

        //    //string result = i.Execute(login, haslo);
        //    //if (!result.Equals("null"))
        //    //{
        //    //    Pracownik pracownik = JsonConvert.DeserializeObject<Pracownik>(result);
        //    //    pracownik.idPracownika.Should().BeGreaterOrEqualTo(1);
        //    //}
        //    //else
        //    //{
        //    //    result.ShouldBeEquivalentTo("null");
        //    //}
        //}

        //[Test]
        //[TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeClientCases))]
        //public void ChangeClient(Klient klient)
        //{
        //    bool result = comm.ChangeClient(klient);

        //    if (klient.idKlienta == 1)
        //    {
        //        result.Should().BeTrue();
        //    }

        //    if (klient.idKlienta == 999)
        //    {
        //        result.Should().BeFalse();
        //    }
        //}

        //Test cases
        public static IEnumerable<Kategoria> TestCategory = new List<Kategoria>()
        {
            new Kategoria { idKategorii = 1, Nazwa = "Komputery"},
            new Kategoria { idKategorii = 999, Nazwa = "Test"}
        };

        public static IEnumerable<Klient> TestClient = new List<Klient>()
        {
            new Klient { idKlienta = 1, Imie ="Mariusz", Nazwisko ="Cebula", Nazwa_firmy = "Cebulex", idAdresu = 3 },
            new Klient { idKlienta = 999, Imie ="Test", Nazwisko ="Test", Nazwa_firmy = "Test", idAdresu = 3 }
        };

        public static IEnumerable<Pracownik> TestEmployee= new List<Pracownik>()
        {
            new Pracownik { Haslo="ala",idPracownika=1,idAdresu=1,Imie="Marek",Login="Kolek",Nazwisko="Stopolski",Sudo=0,Wiek=45},
             new Pracownik { Haslo="elokiu",idPracownika=2,idAdresu=2,Imie="Gierwant",Login="Ekstruder123",Nazwisko="Koniecpolski",Sudo=1,Wiek=99}
        };

        public static IEnumerable<Artykul> TestItem = new List<Artykul>()
        {
            new Artykul { Cena=55,idArtykulu=1,idKategorii=1,Ilosc=98,Nazwa="Ukulele"},
            new Artykul { Cena=35,idArtykulu=2,idKategorii=3,Ilosc=5,Nazwa="Szkielet"},
            new Artykul { Cena=95,idArtykulu=666,idKategorii=56,Ilosc=48,Nazwa="Gramofon"}
        };

        public static IEnumerable<Transakcja> TestTransaction = new List<Transakcja>()
        {
            new Transakcja {idTransakcji = 2 , Data = new System.DateTime(2017,06,01,13,45,30), idKlienta = 1, idPracownika = 3, idDostawcy =1},
            new Transakcja {idTransakcji = 9999, Data = new System.DateTime(2017,06,01,13,45,30), idKlienta = 1, idPracownika = 3, idDostawcy =1}
        };

       

       
    }
}