using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Helper;
using HLCardGame.Infrastructure.Mappers;
using HLCardGame.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace HLCardGame.Application.Tests
{
    [TestClass]
    public class DeckServiceTests
    {
        private Mock<IDeckRepository> _deckRepository;
        private IDeckService _deckService;

        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
            _deckRepository = new Mock<IDeckRepository>();
            _deckService = new DeckService(_deckRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenParametersAreNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            new DeckService(null);
        }

        [TestMethod]
        public async Task CancelDeckAsync_ShouldReturnNumberOFDeletedItemsAsync()
        {
            // Arrange
            var expectedResult = _fixture.Create<int>();

            _deckRepository
                .Setup(a => a.DeleteAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _deckService.CancelDeckAsync(Guid.NewGuid());

            expectedResult
                .Should()
                .Be(result);
        }

        [TestMethod]
        public async Task GetDeckIdByIdAsync_ShouldReturnValidDeck()
        {
            // Arrange
            var deck = _fixture.Create<DeckEntity>();
            var expectedResult = deck.ToModel();

            _deckRepository
                .Setup(a => a.GetByDeckByIdWithCardsAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _deckService.GetDeckIdByIdAsync(deck.DeckId);

            // Assert
            expectedResult
                .Should()
                .Be(result);
        }

        [ExpectedException(typeof(Exception), "Deck not found or finished")]
        [TestMethod]
        public async Task GetDeckIdByIdAsync_ForNoneExistDeck_ShouldReturnNotFoundDeck()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();

            var deck = _fixture
                .Build<DeckEntity>()
                .Without(a => a.Cards)
                .Create();

            _deckRepository
               .Setup(a => a.GetByDeckByIdWithCardsAsync(It.IsAny<Guid>()))
               .Returns(Task.FromResult(deck.ToModel()));

            // Act
            var result = await _deckService.GetDeckIdByIdAsync(deckId);
        }

        [ExpectedException(typeof(Exception), "no more cards to withdraw")]
        [TestMethod]
        public async Task GetDeckIdByIdAsync_ForNoCards_ShouldReturnNotFoundDeck()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();

            // Act
            await _deckService.GetDeckIdByIdAsync(deckId);
        }

        [TestMethod]
        public async Task CreateDeckAsync_ShouldReturnNewDeck()
        {
            // Arrange
            var nPlayer = _fixture.Create<int>();

            _deckRepository
                .Setup(a => a.CreateAsync(It.IsAny<Deck>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _deckService.CreateDeckAsync(nPlayer);

            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .NPlayers
                .Should()
                .Be(nPlayer);
        }

        [TestMethod]
        public async Task GuessNextCardAsync_CheckLower_ShouldReturnValidResult()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();

            var oldDeckCard = _fixture
              .Build<Card>()
              .Create();

            var deck = _fixture
                .Build<DeckEntity>()
                .With(a => a.DeckCardJson, JsonConvert.SerializeObject(oldDeckCard))
                .With(a => a.DeckCardValue, oldDeckCard.Value)
                .Create();

            _deckRepository
                .Setup(a => a.GetByDeckByIdWithCardsAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(deck.ToModel()));

            // Act
            var result = await _deckService.GuessNextCardAsync(deckId, GuessDirection.Lower);

            // Assert
            result
               .Should()
               .NotBeNull();

            result
                .Item1
                .Should()
                .Be(result.Item2.Value >= result.Item3.Value);

            result
                .Item2
                .Should()
                .BeEquivalentTo(oldDeckCard);

            result
                .Item3
                .Should()
                .BeEquivalentTo(deck.Cards.FirstOrDefault().ToModel());
        }

        [TestMethod]
        public async Task GuessNextCardAsync_CheckHigher_ShouldReturnValidResult()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();
            var oldDeckCard = _fixture
                .Build<Card>()
                .Create();

            var deck = _fixture
                .Build<DeckEntity>()
                .With(a => a.DeckCardJson, JsonConvert.SerializeObject(oldDeckCard))
                .With(a => a.DeckCardValue, oldDeckCard.Value)
                .Create();

            _deckRepository
                .Setup(a => a.GetByDeckByIdWithCardsAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(deck.ToModel()));

            // Act
            var result = await _deckService.GuessNextCardAsync(deckId, GuessDirection.Higher);

            // Assert
            result
               .Should()
               .NotBeNull();

            result
                .Item1
                .Should()
                .Be(result.Item2.Value <= result.Item3.Value);

            result
                .Item2
                .Should()
                .BeEquivalentTo(oldDeckCard);

            result
                .Item3
                .Should()
                .BeEquivalentTo(deck.Cards.FirstOrDefault().ToModel());
        }

        [TestMethod]
        public async Task DrawCardAsync_ShouldReturnValidCard()
        {
            // Arrange
            var deckId = _fixture.Create<Guid>();
            var deck = _fixture.Create<DeckEntity>();

            _deckRepository
                .Setup(a => a.GetByDeckByIdWithCardsAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(deck.ToModel()));

            // Act
            var result = await _deckService.DrawCardAsync(deckId);

            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .Should()
                .BeEquivalentTo(deck.Cards.ToList()[0].ToModel());
        }
    }
}