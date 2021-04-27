using System.Linq;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;

namespace HLCardGame.Infrastructure.Mappers
{
    public static class DeckMapper
    {
        public static DeckEntity ToEntity(this Deck deck)
        {
            if (deck is null)
            {
                return default;
            }

            return new DeckEntity()
            {
                DeckId = deck.DeckId,
                NPlayers = deck.NPlayers,
                PlayerTurn = deck.PlayerTurn,
                DeckCardValue = deck.DeckCardValue,
                DeckCardJson = deck.DeckCardJson,
                Cards = deck.Cards.Select(a => a.ToEntity()).ToList()
            };
        }

        public static Deck ToModel(this DeckEntity deck)
        {
            if (deck is null)
            {
                return default;
            }

            return new Deck(
                deckId: deck.DeckId,
                nPlayers: deck.NPlayers,
                playerTurn: deck.PlayerTurn,
                deckCardValue: deck.DeckCardValue,
                deckCardJson: deck.DeckCardJson,
                cards: deck.Cards?.Select(a => a.ToModel()).ToList()
                );
        }
    }
}