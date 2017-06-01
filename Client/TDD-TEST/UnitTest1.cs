using Client.Communication;
using Client.Model;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using PluginExecutor;
using System.Collections.Generic;
using System.Linq;

namespace TDD_TEST
{
    [TestFixture]
    public class UnitTest1
    {
        private ICommunication comm;

        [SetUp]
        public void Setup()
        {
            comm = Communicator.GetInstance();
            comm.SetUrlAddress("http://c414305-001-site1.btempurl.com");
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
            List<Artykul> result = comm.GetItems().ToList();
            result.Count().Should().Equals(6);
        }

        [Test]
        public void GetTransItems()
        {
            List<Artykul_w_transakcji> result = comm.GetTransItems().ToList();
            result.Count().Should().Equals(6);
        }

        [Test]
        public void GetSuppliers()
        {
            IEnumerable<Dostawca> result = comm.GetSuppliers();
            result.Should().HaveCount(x => x > 5);
        }

        [Test]
        public void ChangeItems()
        {
            bool result = comm.ChangeItem(new Artykul { idArtykulu = 999, Nazwa = "LG OLED55B6J", Ilosc = 35, idKategorii = 5, Cena = 11799M });
            result.Should().BeFalse();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(null)]
        public void GetAddress(int id)
        {
            Adres result = comm.GetAddress(id);

            if (id == 1)
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
        }

        [Test]
        [TestCase(2)]
        [TestCase(null)]
        public void GetTransaction(int id)
        {
            Transakcja result = comm.GetTransaction(id);

            if (id == 2)
            {
                result.Should().Equals(new Transakcja
                {
                    idTransakcji = 2,
                    Data = new System.DateTime(2017, 06, 01, 13, 45, 30),
                    idKlienta = 1,
                    idPracownika = 3,
                    idDostawcy = 1
                });
            }

            if (id == 0)
            {
                result.Should().Equals(null);
            }
        }

        [Test]
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeAddressCases))]
        public void ChangeAddress(Adres adres)
        {
            bool result = comm.ChangeAddress(adres);

            if (adres.idAdresu == 1)

            {
                result.Should().BeTrue();
            }
            if (adres.idAdresu == 999)
            {
                result.Should().BeFalse();
            }
        }

        [Test]
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestRegisterSupplyCases))]
        public void RegisterSupply(Dostawca dostawca)
        {
            bool result = comm.RegisterSupplier(dostawca);

            if (dostawca.idDostawcy == 1)
            {
                result.ShouldBeEquivalentTo(false);
            }

            if (dostawca.idDostawcy == 999)
            {
                result.ShouldBeEquivalentTo(true);
            }
        }

        [Test]
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

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(null)]
        public void GetSupplier(int id)
        {
            Dostawca result = comm.GetSupplier(id);

            if (id == 1)
            {
                result.ShouldBeEquivalentTo(new Dostawca
                {
                    idDostawcy = 1,
                    Nazwa = "Kurierex"
                });
            }

            if (id == 0)
            {
                result.ShouldBeEquivalentTo(null);
            }
        }

        [Test]
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestRegisterTransactionCases))]
        public void RegisterTransaction(Transakcja transakcja)
        {
            int result = comm.RegisterTransaction(transakcja);

            if (transakcja.idTransakcji == 1)
            {
                result.ShouldBeEquivalentTo(1);
            }
            else result.Should().BeGreaterThan(0);
        }

        [Test]
        [TestCase("a", "b")]
        [TestCase("RogalDDL", "P@ssw0rd")]
        public void Login(string login, string haslo)
        {
            IPluginLogin i = new PluginLogin();

            string result = i.Login(login, haslo);
            if (!result.Equals("null"))
            {
                Pracownik pracownik = JsonConvert.DeserializeObject<Pracownik>(result);
                pracownik.idPracownika.Should().BeGreaterOrEqualTo(1);
            }
            else
            {
                result.ShouldBeEquivalentTo("null");
            }
        }

        [Test]
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeClientCases))]
        public void ChangeClient(Klient klient)
        {
            bool result = comm.ChangeClient(klient);

            if (klient.idKlienta == 1)
            {
                result.Should().BeTrue();
            }

            if (klient.idKlienta == 999)
            {
                result.Should().BeFalse();
            }
        }

        //Test cases
        public static Kategoria[] TestChangeCategoryCases =
        {
            new Kategoria { idKategorii = 1, Nazwa = "Komputery"},
            new Kategoria { idKategorii = 999, Nazwa = "Test"}
        };

        public static Dostawca[] TestChangeSupplierCases =
        {
            new Dostawca { idDostawcy = 1, Nazwa = "Kuries"},
            new Dostawca { idDostawcy = 999, Nazwa = "Test"}
        };

        public static Transakcja[] TestRegisterTransactionCases =
        {
            new Transakcja {idTransakcji = 2 , Data = new System.DateTime(2017,06,01,13,45,30), idKlienta = 1, idPracownika = 3, idDostawcy =1},
            new Transakcja {idTransakcji = 9999, Data = new System.DateTime(2017,06,01,13,45,30), idKlienta = 1, idPracownika = 3, idDostawcy =1}
        };

        public static Adres[] TestChangeAddressCases =
        {
            new Adres { idAdresu = 1, Miejscowosc = "Czeladz", Kod_pocztowy = "41-250", Wojewodztwo = "Slaskie" },
            new Adres { idAdresu = 999, Miejscowosc = "Testowa", Kod_pocztowy = "11-111", Wojewodztwo = "Slaskie" }
        };

        public static Dostawca[] TestRegisterSupplyCases =
        {
            new Dostawca { idDostawcy = 1, Nazwa = "Kuriers"},
            new Dostawca { idDostawcy= 999, Nazwa="999"}
        };

        public static Klient[] TestChangeClientCases =
        {
            new Klient { idKlienta = 1, Imie ="Mariusz", Nazwisko ="Cebula", Nazwa_firmy = "Cebulex", idAdresu = 3 },
            new Klient { idKlienta = 999, Imie ="Test", Nazwisko ="Test", Nazwa_firmy = "Test", idAdresu = 3 }
        };
    }
}