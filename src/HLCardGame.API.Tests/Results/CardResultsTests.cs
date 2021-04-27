using AutoFixture;
using HLCardGame.API.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.API.Tests.Results
{
    [TestClass]
    public class CardResultsTests
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
            var color = _fixture.Create<string>();
            var suit = _fixture.Create<string>();
            var value = _fixture.Create<int>();
            var displayName = _fixture.Create<string>();

            // Act
            var CardResults = new CardResults(
                color: color,
                suit: suit,
                value: value,
                displayName: displayName);

            // Assert
            Assert.AreEqual(CardResults.Color, color);
            Assert.AreEqual(CardResults.Suit, suit);
            Assert.AreEqual(CardResults.Value, value);
            Assert.AreEqual(CardResults.DisplayName, displayName);
        }
    }
}