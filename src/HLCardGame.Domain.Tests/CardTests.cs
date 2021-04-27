using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Domain.Tests
{
    [TestClass]
    public class CardTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Card_ShouldReturnCorrectly()
        {
            // Arrange
            var cardId = _fixture.Create<Guid>();
            var color = _fixture.Create<string>();
            var suit = _fixture.Create<string>();
            var value = _fixture.Create<int>();
            var displayName = _fixture.Create<string>();

            // Act
            var Results = new Card(
                cardId: cardId,
                color: color,
                suit: suit,
                value: value,
                displayName: displayName);

            // Assert
            Assert.AreEqual(Results.CardId, cardId);
            Assert.AreEqual(Results.Color, color);
            Assert.AreEqual(Results.Suit, suit);
            Assert.AreEqual(Results.Value, value);
            Assert.AreEqual(Results.DisplayName, displayName);
        }
    }
}