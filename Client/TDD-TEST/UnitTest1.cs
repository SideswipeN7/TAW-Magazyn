using NUnit.Framework;
using Client.Communication;
using Client.Model;
using System.Collections.Generic;
using FluentAssertions;
using System.Threading;
using System;

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
            result.Should().Equals(new List<Kategoria> {new Kategoria {idKategorii=1,Nazwa="Komputery" },
           new Kategoria {idKategorii=2,Nazwa="Telefony" },
           new Kategoria {idKategorii=3,Nazwa="Aparaty"},
           new Kategoria {idKategorii=4,Nazwa="Pralki" },
           new Kategoria {idKategorii=5,Nazwa="Telewizory" } });
        }

        [Test]
        public void GetItems()
        {
            IEnumerable<Artykul> result = comm.GetItems();
            result.Should().Equals(new List<Artykul> {new Artykul {idArtykulu=2,Nazwa="Lenovo H50-55",Ilosc=12,idKategorii=1,Cena=1523.37M },
           new Artykul {idArtykulu=3,Nazwa="Xiaomi Redmi 3S",Ilosc=50,idKategorii=2,Cena=899M },
           new Artykul {idArtykulu=4,Nazwa="Nikon D7100",Ilosc=50,idKategorii=3,Cena=3150M},
           new Artykul {idArtykulu=5,Nazwa="Amica AWB510L",Ilosc=35,idKategorii=4,Cena=799.99M },
           new Artykul {idArtykulu=6,Nazwa="LG OLED55B6J",Ilosc=12,idKategorii=5,Cena=11799M }});
        }

        [Test]
        public void GetTransactions()
        {
            IEnumerable<Transakcja> result = comm.GetTransactions();
            result.Should().Equals(new List<Transakcja> {
                new Transakcja { idTransakcji = 2, Data = new DateTime (2017,06,01,13,45,30), idKlienta = 1, idPracownika = 3, idDostawcy = 1 },
                new Transakcja { idTransakcji = 3, Data = new DateTime (2017,06,12,12,30,30), idKlienta = 2, idPracownika = 1, idDostawcy = 2 },
                new Transakcja { idTransakcji = 4, Data = new DateTime (2017,07,01,08,45,00), idKlienta = 3, idPracownika = 2, idDostawcy = 3 },
                new Transakcja { idTransakcji = 5, Data = new DateTime (2017,07,10,12,00,00), idKlienta = 4, idPracownika = 4, idDostawcy = 4 },
                new Transakcja { idTransakcji = 6, Data = new DateTime (2017,12,01,20,45,30), idKlienta = 5, idPracownika = 5, idDostawcy = 5 }

          });
        }
    }
}
