using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLCardGame.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class DeckRepositoryTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task GetByDeckByIdWithCardsAsync_Should_ReturnDeckInfo()
        {
            // Arrange
            var dbContext = InMemoryDbContext.CreateDbContext();
            var deck = _fixture.Create<DeckEntity>();
            dbContext.Add(deck);
            dbContext.SaveChanges();

            // Act
            var deckRepository = new DeckRepository(dbContext);
            await deckRepository.GetByIdAsync(deck.DeckId);

            // Assert
            var result = dbContext.Find<DeckEntity>(deck.DeckId);
            result
                .Should()
                .NotBeNull();

            result
                .Should()
                .BeEquivalentTo(deck);
        }

        [TestMethod]
        public async Task DeleteAsyncDeckAsync_Should_ReturnOk()
        {
            // Arrange
            var dbContext = InMemoryDbContext.CreateDbContext();
            var deck = _fixture.Create<DeckEntity>();
            var expectedNumberOfDeletion = 1 + deck.Cards.Count;
            dbContext.Add(deck);
            dbContext.SaveChanges();

            // Act
            var deckRepository = new DeckRepository(dbContext);
            var result = await deckRepository.DeleteAsync(deck.DeckId);

            // Assert
            Assert.AreEqual(result, expectedNumberOfDeletion);
        }
    }
}