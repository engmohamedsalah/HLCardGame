using System;
using System.Linq;
using System.Threading.Tasks;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Helper;
using HLCardGame.Infrastructure.Repositories;
using Newtonsoft.Json;

namespace HLCardGame.Application
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;

        public DeckService(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository
                 ?? throw new ArgumentNullException(nameof(deckRepository));
        }

        public Task<int> CancelDeckAsync(Guid deckId)
        {
            return _deckRepository.DeleteAsync(deckId);
        }

        public async Task<Deck> GetDeckIdByIdAsync(Guid deckId)
        {
            return await GetDeckAsync(deckId);
        }

        public async Task<Deck> CreateDeckAsync(int nPlayer)
        {
            //create 52 card
            var cards = CardsGenerator.CreateCards();
            var deckId = Guid.NewGuid();

            var newDeck = new Deck(
                deckId: deckId,
                nPlayers: nPlayer,
                playerTurn: 0,
                deckCardValue: 0,
                deckCardJson: "{}",
                cards: cards);

            await _deckRepository.CreateAsync(newDeck);

            return (newDeck);
        }

        public async Task<(bool, Card, Card)> GuessNextCardAsync(Guid deckId, GuessDirection guessDirection)
        {
            var deck = await GetDeckAsync(deckId);

            var newDeckCard = deck.Cards?.ToList().FirstOrDefault();

            if (newDeckCard == null)
                throw new Exception("this Deck contains no cards");

            if (deck.DeckCardValue == 0)
                throw new Exception("game did not started yet");

            deck.Cards.Remove(newDeckCard);

            //calculate the Guess Result
            var GuessResult = (
               (guessDirection == GuessDirection.Higher && deck.DeckCardValue <= newDeckCard.Value)
               ||
               (guessDirection == GuessDirection.Lower && deck.DeckCardValue >= newDeckCard.Value)
           );

            //now the top card will be on the deck
            deck.DeckCardValue = newDeckCard.Value;
            var oldCard = JsonConvert.DeserializeObject<Card>(deck.DeckCardJson);
            deck.DeckCardJson = JsonConvert.SerializeObject(newDeckCard);

            //get the next player turn
            deck.PlayerTurn = (deck.PlayerTurn + 1) % (deck.NPlayers + 1);

            await _deckRepository.UpdateDeckCardAsync(deck);

            return (GuessResult, oldCard, newDeckCard);
        }

        public async Task<Card> DrawCardAsync(Guid deckId)
        {
            var deck = await GetDeckAsync(deckId);
            if (deck == null)
                throw new Exception("this deck is not exist");

            var card = deck.Cards?.ToList().FirstOrDefault();

            if (card == null)
                throw new Exception("this Deck contains no cards");

            //now the top card will be on the deck
            deck.Cards.Remove(card);
            //if the game is not started yet it will be started
            if (deck.PlayerTurn == 0)
            {
                deck.PlayerTurn++;
            }

            deck.DeckCardValue = card.Value;
            deck.DeckCardJson = JsonConvert.SerializeObject(card);
            await _deckRepository.UpdateDeckCardAsync(deck);
            return card;
        }

        private async Task<Deck> GetDeckAsync(Guid deckId)
        {
            var deck = await _deckRepository.GetByDeckByIdWithCardsAsync(deckId);
            if (deck == null)
                throw new Exception("Deck not found or finished");

            if (deck.Cards == null || deck.Cards.Count == 0)
                throw new Exception("no more cards to withdraw");

            return deck;
        }
    }
}