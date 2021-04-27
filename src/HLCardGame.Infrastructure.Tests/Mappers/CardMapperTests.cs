using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Infrastructure.Tests.Mappers
{
    [TestClass]
    public class CardMapperTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }
    }
}