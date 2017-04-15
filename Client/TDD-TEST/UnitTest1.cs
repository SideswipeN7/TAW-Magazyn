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
        public void ChangeItems()
        {
            bool result = comm.ChangeItem(new Artykul { idArtykulu = 999, Nazwa = "LG OLED55B6J", Ilosc = 35, idKategorii = 5, Cena = 11799M });
            result.Should().BeFalse();
        }

        [Test]
        [TestCaseSource(typeof(UnitTest1), nameof(UnitTest1.TestChangeSupplierCases))]
        public void ChangeSupplier(Dostawca dostawca)
        {
            bool result = comm.ChangeSupplier(dostawca);

            if (dostawca.idDostawcy == 1)
            {
                result.Should().BeTrue();
            }

            if (dostawca.idDostawcy == 999)
            {
                result.Should().BeFalse();
            }
        }

        //Test cases
        public static Dostawca[] TestChangeSupplierCases =
        {
            new Dostawca { idDostawcy = 1, Nazwa = "Kuries"},
            new Dostawca { idDostawcy = 999, Nazwa = "Test"}

        };
    }
}
