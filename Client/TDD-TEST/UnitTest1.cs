using NUnit.Framework;
using Client.Communication;
using Client.Model;
using System.Collections.Generic;
using FluentAssertions;

namespace TDD_TEST
{

    [TestFixture]
    public class UnitTest1
    {

        ICommunication comm;
        [SetUp]
        public void Setup()
        {
            comm = new Communicator();
            comm.SetUrlAddress("http://o1018869-001-site1.htempurl.com");
        }


        [Test]
        public void GetCategories()
        {
            IEnumerable<Kategoria> result = comm.GetCategories();
            result.ShouldBeEquivalentTo(new List<Kategoria> {new Kategoria {idKategorii=1,Nazwa="Komputery" },
           new Kategoria {idKategorii=2,Nazwa="Telefony" },
           new Kategoria {idKategorii=3,Nazwa="Aparaty"},
           new Kategoria {idKategorii=4,Nazwa="Pralki" },
           new Kategoria {idKategorii=5,Nazwa="Telewizory" } });
        }

        [Test]
        public void GetItems()
        {
            IEnumerable<Artykul> result = comm.GetItems();
            result.ShouldBeEquivalentTo(new List<Artykul> {
           new Artykul {idArtykulu=2,Nazwa="Lenovo H50-55",Ilosc=12,idKategorii=1,Cena=1523.37M },
           new Artykul {idArtykulu=3,Nazwa="Xiaomi Redmi 3S",Ilosc=50,idKategorii=2,Cena=899M },
           new Artykul {idArtykulu=4,Nazwa="Nikon D7100",Ilosc=50,idKategorii=3,Cena=3150M},
           new Artykul {idArtykulu=5,Nazwa="Amica AWB510L",Ilosc=35,idKategorii=4,Cena=799.99M },
           new Artykul {idArtykulu=6,Nazwa="LG OLED55B6J",Ilosc=35,idKategorii=5,Cena=11799M }});
        }

        [Test]
        public void GetTransItems()
        {
            IEnumerable<Artykul_w_transakcji> result = comm.GetTransItems();
            result.ShouldBeEquivalentTo(new List<Artykul_w_transakcji> {
                new Artykul_w_transakcji { idArt_w_trans = 2, Cena = 1523.37M, idTransakcji = 2, idArtykulu = 2 },
                new Artykul_w_transakcji { idArt_w_trans = 3, Cena = 899M, idTransakcji = 3, idArtykulu = 3},
                new Artykul_w_transakcji { idArt_w_trans = 4, Cena = 3150M, idTransakcji = 4, idArtykulu = 4},
                new Artykul_w_transakcji { idArt_w_trans = 5, Cena = 799.99M, idTransakcji = 5, idArtykulu = 5},
                new Artykul_w_transakcji { idArt_w_trans = 6, Cena = 11799M, idTransakcji = 6, idArtykulu = 6}
           });
        }

        [Test]
        public void ChangeItems()
        {
            bool result = comm.ChangeItem(new Artykul { idArtykulu = 999, Nazwa = "LG OLED55B6J", Ilosc = 35, idKategorii = 5, Cena = 11799M });
            result.Should().BeFalse();
        }

        [Test]
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(null)]
        public void GetAddress(int id)
        {
            Adres result = comm.GetAddress(id);

            if(id == 1)
            {
                result.ShouldBeEquivalentTo(new Adres
                {
                    idAdresu = 1,
                    Miejscowosc = "Czeladz",
                    Kod_pocztowy = "41-250",
                    Wojewodztwo = "Slaskie"
                });
            }

            if (id == 0)
            {
                result.ShouldBeEquivalentTo(null);
            }

            if (id == null)
            {
                result.ShouldBeEquivalentTo(null);
            }
        }
=======
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeCategoryCases))]
        public void ChangeCategory(Kategoria kategoria)
        {
            bool result = comm.ChangeCategory(kategoria);

            if (kategoria.idKategorii == 1)
=======
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeSupplierCases))]
        public void ChangeSupplier(Dostawca dostawca)
        {
            bool result = comm.ChangeSupplier(dostawca);

            if (dostawca.idDostawcy == 1)
>>>>>>> origin/Method-ChangeSupplier-
=======
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeAddressCases))]
        public void ChangeAddress(Adres adres)
        {
            bool result = comm.ChangeAddress(adres);

            if (adres.idAdresu == 1)
>>>>>>> Method-ChangeAddress
            {
                result.Should().BeTrue();
            }

<<<<<<< HEAD
<<<<<<< HEAD
            if (kategoria.idKategorii == 999)
=======
            if (dostawca.idDostawcy == 999)
>>>>>>> origin/Method-ChangeSupplier-
            {
                result.Should().BeFalse();
=======
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestRegisterSupplyCases))]
        public void RegisterSupply(Dostawca dostawca)
        {
            bool result = comm.RegisterSupplier(dostawca);

            if (dostawca.idDostawcy == 1)
            {
                result.ShouldBeEquivalentTo(false);
            }
            
            if(dostawca.idDostawcy == 999)
            {
                result.ShouldBeEquivalentTo(true);
>>>>>>> Method-RegisterSupplier
            }
        }

        //Test cases
<<<<<<< HEAD
<<<<<<< HEAD
        public static Kategoria[] TestChangeCategoryCases =
        {
            new Kategoria { idKategorii = 1, Nazwa = "Komputery"},
            new Kategoria { idKategorii = 999, Nazwa = "Test"}
>>>>>>> origin/Method-ChangeCategory
=======
        public static Dostawca[] TestChangeSupplierCases =
        {
            new Dostawca { idDostawcy = 1, Nazwa = "Kuries"},
            new Dostawca { idDostawcy = 999, Nazwa = "Test"}
>>>>>>> origin/Method-ChangeSupplier-
=======
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeAddressCases))]
        public void RegisterAddress(Adres adres)
        {
            int result = comm.RegisterAddress(adres);

            if (adres.idAdresu == 1)
            {
                result.ShouldBeEquivalentTo(1);
            }

            else result.Should().BeGreaterThan(0);
        }




=======
            if (adres.idAdresu == 999)
            {
                result.Should().BeFalse();
            }
        }

>>>>>>> Method-ChangeAddress
        //Test cases
        public static Adres[] TestChangeAddressCases =
        {
            new Adres { idAdresu = 1, Miejscowosc = "Czeladz", Kod_pocztowy = "41-250", Wojewodztwo = "Slaskie" },
            new Adres { idAdresu = 999, Miejscowosc = "Testowa", Kod_pocztowy = "11-111", Wojewodztwo = "Slaskie" }
<<<<<<< HEAD
>>>>>>> origin/Method-RegisterAddress
=======
        public static Dostawca[] TestRegisterSupplyCases =
        {
            new Dostawca { idDostawcy = 1, Nazwa = "Kuriers"},
            new Dostawca { idDostawcy= 999, Nazwa="999"}

        };
>>>>>>> Method-RegisterSupplier
=======

        };
>>>>>>> Method-ChangeAddress

        };
    }
}
