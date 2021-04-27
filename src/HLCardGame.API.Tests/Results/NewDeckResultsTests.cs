using System;
using AutoFixture;
using HLCardGame.API.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.API.Tests.Results
{
    [TestClass]
    public class NewDeckResultsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void CardResults_ShouldReturnCorrectly()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();
            var remaining = _fixture.Create<int>();
            var card = _fixture.Create<CardResults>();

            // Act
            var Results = new NewDeckResults(
                deckId: deckId,
                remaining: remaining,
                card: card);

            // Assert
            Assert.AreEqual(Results.DeckId, deckId);
            Assert.AreEqual(Results.Remaining, remaining);
            Assert.AreEqual(Results.Card, card);
        }
    }
}