using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using HLCardGame.API.Controllers;
using HLCardGame.API.Results;
using HLCardGame.Application;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HLCardGame.API.Tests.Controllers
{
    [TestClass]
    public class DecksControllerTests
    {
        private static DecksController _controllerUnderTest;
        private Mock<IDeckService> _deckControlService;
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
            _deckControlService = new Mock<IDeckService>();

            _controllerUnderTest = new DecksController(_deckControlService.Object);
        }

        [TestMethod]
        public async Task CreateNew_WithCorrectParameter_ShouldReturnOK()
        {
            //Arrange
            var deck = _fixture.Create<Deck>();
            var nplayer = _fixture.Create<int>();
            _deckControlService
                .Setup(a => a.CreateDeckAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(deck));
            // Act
            var result = await _controllerUnderTest.CreateNewDeck(nplayer)
               as OkObjectResult;
            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.OK);

            var deckResult = result.Value as DeckResults;
            deckResult
                .NPlayer
                .Should()
                .Equals(nplayer);

            deckResult
                .Shuffled
                .Equals(true);
        }

        [TestMethod]
        public async Task CreateNew_WithWrongParameter_ShouldReturnBadRequest()
        {
            //Arrange
            var deck = _fixture.Create<Deck>();
            var nplayer = 0;
            _deckControlService
                .Setup(a => a.CreateDeckAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(deck));
            // Act
            var result = await _controllerUnderTest.CreateNewDeck(nplayer)
               as BadRequestObjectResult;
            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task GetDeckById_WithCorrectParameter_ShouldReturnOk()
        {
            //Arrange
            var deck = _fixture.Create<Deck>();

            _deckControlService
                .Setup(a => a.GetDeckIdByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(deck));
            // Act

            var result = await _controllerUnderTest.GetDeckById(deck.DeckId)
               as OkObjectResult;

            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.OK);

            var deckResult = result.Value as DeckResults;

            deckResult
                .DeckId
                .Should()
                .Be(deck.DeckId);
        }

        [TestMethod]
        public async Task GetDeckById_WithWrongParameter_ShouldReturnNotFound()
        {
            //Arrange
            var deck = _fixture.Create<Deck>();

            _deckControlService
                .Setup(a => a.GetDeckIdByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult((Deck)null));
            // Act

            var result = await _controllerUnderTest.GetDeckById(Guid.NewGuid())
               as NotFoundResult;

            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task DrawCard_WithCorrectParameter_ShouldReturnOk()
        {
            //Arrange
            var card = _fixture.Create<Card>();

            _deckControlService
                .Setup(a => a.DrawCardAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(card));
            // Act

            var result = await _controllerUnderTest.DrawCard(Guid.NewGuid())
               as OkObjectResult;

            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.OK);

            var cardResult = result.Value as CardResults;

            cardResult
                .Suit
                .Should()
                .Be(card.Suit);

            cardResult
                .Value
                .Should()
                .Be(card.Value);

            cardResult
                .Color
                .Should()
                .Be(card.Color);

            cardResult
                .DisplayName
                .Should()
                .Be(card.DisplayName);
        }

        [TestMethod]
        public async Task GuessNextCard_WithCorrectParameter_ShouldReturnOk()
        {
            //Arrange
            var card = _fixture.Create<Card>();
            var deckCard = _fixture.Create<Card>();
            var success = _fixture.Create<bool>();

            var expectedResult = new GuessResults(
                sucess: success,
                deckCard: new CardResults(
                   color: deckCard.Color,
                   suit: deckCard.Suit,
                   value: deckCard.Value,
                   displayName: deckCard.DisplayName),
                card: new CardResults(
                   color: card.Color,
                   suit: card.Suit,
                   value: card.Value,
                   displayName: card.DisplayName));

            _deckControlService
                .Setup(a => a.GuessNextCardAsync(It.IsAny<Guid>(), It.IsAny<GuessDirection>()))
                .Returns(Task.FromResult((success, deckCard, card)));

            // Act
            var result = await _controllerUnderTest.GuessNextCard(Guid.NewGuid(), GuessDirection.Higher)
               as OkObjectResult;

            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.OK);

            result
                .Value
                .Should()
                .BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public async Task DeleteDeck_WithCorrectParameter_ShouldReturnOk()
        {
            //Arrange
            var deckId = _fixture.Create<Guid>();

            _deckControlService
                .Setup(a => a.CancelDeckAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(1));

            // Act
            var result = await _controllerUnderTest.CancelDeck(deckId)
               as OkResult;

            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task DeleteDeck_WithWrongParameter_ShouldReturnNotFound()
        {
            //Arrange
            var deckId = _fixture.Create<Guid>();

            _deckControlService
                .Setup(a => a.CancelDeckAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(0));

            // Act
            var result = await _controllerUnderTest.CancelDeck(deckId)
               as NotFoundResult;

            // Assert
            result
               .Should()
               .NotBeNull();

            result.StatusCode
                .Should()
                .Equals(HttpStatusCode.NotFound);
        }
    }
}