using System;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Domain.Tests
{
    [TestClass]
    public class DeckTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Deck_ShouldReturnCorrectly()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();
            var nPlayers = _fixture.Create<int>();
            var PlayerTurn = _fixture.Create<int>();
            var deckCardValue = _fixture.Create<int>();
            var deckCardJson = "{}";
            var cards = _fixture.CreateMany<Card>(52).ToList();

            // Act
            var Results = new Deck(
                deckId: deckId,
                nPlayers: nPlayers,
                playerTurn: PlayerTurn,
                lastCardValue: deckCardValue,
                deckCardJson: deckCardJson,
                cards: cards);

            // Assert
            Assert.AreEqual(Results.DeckId, deckId);
            Assert.AreEqual(Results.NPlayers, nPlayers);
            Assert.AreEqual(Results.PlayerTurn, PlayerTurn);
            Assert.AreEqual(Results.LastCardValue, deckCardValue);
            Assert.AreEqual(Results.Cards, cards);
        }
    }
}