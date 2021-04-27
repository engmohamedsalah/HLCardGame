using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Infrastructure.Tests.Helper
{
    [TestClass]
    public class CardHelperTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void GetShortName_Value_From_2__To_10_ShouldReturnAsExpectedValue()
        {
            // Arrange
            var numberList = Enumerable.Range(2, 9).ToList();
            var expected = new List<string>();
            var result = new List<string>();

            // Act
            foreach (var n in numberList)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    result.Add(CardHelper.GetShortName(n, suit));
                    expected.Add(n.ToString() + Enum.GetName(typeof(Suit), suit)[0]);
                }
            }

            // Assert
            expected
                .Should()
                .BeEquivalentTo(result);
        }

        [TestMethod]
        public void GetShortName_Other_Cards_ShouldReturnAsExpectedValue()
        {
            // Arrange
            var ace = 14;
            var jack = 11;
            var queen = 12;
            var king = 13;
            var suit = _fixture.Create<Suit>();
            var firstCharOfSuit = Enum.GetName(typeof(Suit), suit)[0];

            // Act
            var aceResult = CardHelper.GetShortName(ace, suit);
            var jackResult = CardHelper.GetShortName(jack, suit);
            var queenResult = CardHelper.GetShortName(queen, suit);
            var kingResult = CardHelper.GetShortName(king, suit);

            // Assert
            aceResult
                .Should()
                .Be("A" + firstCharOfSuit);

            jackResult
                .Should()
                .Be("J" + firstCharOfSuit);

            queenResult
                .Should()
                .Be("Q" + firstCharOfSuit);

            kingResult
                .Should()
                .Be("K" + firstCharOfSuit);
        }
    }
}