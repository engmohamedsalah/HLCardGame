using System.Linq;
using AutoFixture;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Application.Tests
{
    [TestClass]
    public class CardsGeneratorTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void CreateCards_ShouldReturn52()
        {
            // Act
            var result = CardsGenerator.CreateCards();

            // Assert
            Assert.AreEqual(result.Count, 52);
            Assert.AreEqual(result.Count(a => a.Color == CardColor.Black.ToString()), 26);
            Assert.AreEqual(result.Count(a => a.Color == CardColor.Red.ToString()), 26);
            Assert.AreEqual(result.Count(a => a.Suit == Suit.Clubs.ToString()), 13);
            Assert.AreEqual(result.Count(a => a.Suit == Suit.Diamonds.ToString()), 13);
            Assert.AreEqual(result.Count(a => a.Suit == Suit.Hearts.ToString()), 13);
            Assert.AreEqual(result.Count(a => a.Suit == Suit.Spades.ToString()), 13);
            for (int i = 2; i <= 14; i++)
            {
                Assert.AreEqual(result.Count(a => a.Value == i), 4);
            }
        }

        [TestMethod]
        public void ShuffleCards_ShouldReturnShuffeldCards()
        {
            // Arrange
            var cards = _fixture.CreateMany<Card>(52).ToList();
            Card[] originalCopy = new Card[52];
            cards.CopyTo(originalCopy);

            // Act
            CardsGenerator.ShuffleCards(cards);

            // Assert
            var diff = 0;
            for (int i = 0; i < 52; i++)
            {
                if (cards[i].Value != originalCopy[i].Value)
                    diff++;
            }
            Assert.AreNotEqual(diff, 0);
        }
    }
}