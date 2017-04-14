using NUnit.Framework;
using Client.Communication;
using Client.Model;
using System.Collections.Generic;
using FluentAssertions;
using System.Threading;

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
            Thread.Sleep(500);
            result.Should().Equals(new List<Kategoria> {new Kategoria {idKategorii=1,Nazwa="Komputery" },
           new Kategoria {idKategorii=2,Nazwa="Telefony" },
           new Kategoria {idKategorii=3,Nazwa="Aparaty"},
           new Kategoria {idKategorii=4,Nazwa="Pralki" },
           new Kategoria {idKategorii=5,Nazwa="Telewizory" } });
        }

    }
}
