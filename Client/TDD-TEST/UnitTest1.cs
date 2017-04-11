using NUnit.Framework;
using Client.Communication;
using Client.Model;
using System.Collections.Generic;
using FluentAssertions;
using System.Threading;

namespace TDD_TEST
{


    public class UnitTest1
    {
        [Test]

        public void GetCategories()
        {
            var GetCategories = new Communicator();
            IEnumerable<Kategoria> result = GetCategories.GetCategories();
            Thread.Sleep(500);
            result.Should().Equals(new List<Kategoria> {new Kategoria {idKategorii=1,Nazwa="Komputery" },
           new Kategoria {idKategorii=2,Nazwa="Telefony" },
           new Kategoria {idKategorii=3,Nazwa="Aparaty"},
           new Kategoria {idKategorii=4,Nazwa="Pralki" },
           new Kategoria {idKategorii=5,Nazwa="Telewizory" } });
        }

    }
}
