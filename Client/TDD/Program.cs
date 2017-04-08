using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Client;

namespace TDD
{
    [TestFixture]
    class Program
    {
        //
        int mock;
        [SetUp]
        public void Setup()
        {
            mock = 0;
        }

        static void Main(string[] args)
        {
        }
    }
}
