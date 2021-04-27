using AutoFixture;
using HLCardGame.API.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.API.Tests.Results
{
    [TestClass]
    public class GuessResultsTests
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
            var sucess = _fixture.Create<bool>();
            var card = _fixture.Create<CardResults>();
            var deckCard = _fixture.Create<CardResults>();

            // Act
            var Results = new GuessResults(
                sucess: sucess,
                deckCard: deckCard,
                card: card);

            // Assert
            Assert.AreEqual(Results.Sucess, sucess);
            Assert.AreEqual(Results.Card, card);
            Assert.AreEqual(Results.DeckCard, deckCard);
        }
    }
}