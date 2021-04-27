using System;
using AutoFixture;
using HLCardGame.API.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.API.Tests.Results
{
    [TestClass]
    public class DeckResultsTests
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
            var shuffled = _fixture.Create<bool>();
            var nPlayer = _fixture.Create<int>();
            var playerTurn = _fixture.Create<int>();
            var remaining = _fixture.Create<int>();

            // Act
            var Results = new DeckResults(
                deckId: deckId,
                shuffled: shuffled,
                nPlayer: nPlayer,
                playerTurn: playerTurn,
                remaining: remaining);

            // Assert
            Assert.AreEqual(Results.DeckId, deckId);
            Assert.AreEqual(Results.Shuffled, shuffled);
            Assert.AreEqual(Results.NPlayer, nPlayer);
            Assert.AreEqual(Results.PlayerTurn, playerTurn);
            Assert.AreEqual(Results.Remaining, remaining);
        }
    }
}