using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Infrastructure.Tests.Mappers
{
    [TestClass]
    public class DeckMapperTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void ToModel_WithValidObject_ShouldBeExecuteCorrectly()
        {
            // Arrange
            var deckEntity = _fixture.Create<DeckEntity>();

            var expected = new Deck(
                deckId: deckEntity.DeckId,
                nPlayers: deckEntity.NPlayers,
                playerTurn: deckEntity.PlayerTurn,
                lastCardValue: deckEntity.LastCardValue,
                deckCardJson: deckEntity.LastCardJson,
                cards: deckEntity.Cards.Select(a => a.ToModel()).ToList());

            // Act
            var result = deckEntity.ToModel();

            // Assert
            result
                .Should()
                .BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ToModel_WithNullObject_ShouldBeExecuteCorrectly()
        {
            // Arrange
            var deckEntity = null as DeckEntity;

            // Act
            var result = deckEntity.ToModel();

            // Assert
            result
                .Should()
                .BeNull();
        }

        [TestMethod]
        public void ToToEntity_WithValidObject_ShouldBeExecuteCorrectly()
        {
            // Arrange
            var deck = new Deck(
                deckId: Guid.NewGuid(),
                nPlayers: _fixture.Create<int>(),
                playerTurn: _fixture.Create<int>(),
                lastCardValue: _fixture.Create<int>(),
                deckCardJson: "{}",
                cards: GenerateRandomCards(52).ToList());

            var expected = new DeckEntity()
            {
                DeckId = deck.DeckId,
                NPlayers = deck.NPlayers,
                PlayerTurn = deck.PlayerTurn,
                LastCardValue = deck.LastCardValue,
                LastCardJson = deck.LastCardJson,
                Cards = deck.Cards.Select(a => a.ToEntity()).ToList(),
            };

            // Act
            var result = deck.ToEntity();

            // Assert
            result
                .Should()
                .BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ToEntity_WithNullObject_ShouldBeExecuteCorrectly()
        {
            // Arrange
            var deck = null as Deck;

            // Act
            var result = deck.ToEntity();

            // Assert
            result
                .Should()
                .BeNull();
        }

        private Card GenerateRandomCard()
        {
            return new Card(
                cardId: Guid.NewGuid(),
                 color: _fixture.Create<CardColor>().ToString(),
                 suit: _fixture.Create<Suit>().ToString(),
                 value: _fixture.Create<int>(),
                 displayName: _fixture.Create<string>().ToString());
        }

        private IEnumerable<Card> GenerateRandomCards(int num)
        {
            for (int i = 0; i < num; i++)
                yield return GenerateRandomCard();
        }
    }
}