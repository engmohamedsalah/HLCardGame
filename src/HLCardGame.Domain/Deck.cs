using System;
using System.Collections.Generic;

namespace HLCardGame.Domain
{
    public class Deck
    {
        public Deck(
            Guid deckId,
            int nPlayers,
            int playerTurn,
            int deckCardValue,
            string deckCardJson,
            List<Card> cards)
        {
            DeckId = deckId;
            NPlayers = nPlayers;
            PlayerTurn = playerTurn;
            DeckCardValue = deckCardValue;
            Cards = cards;
            DeckCardJson = deckCardJson;
        }

        public Guid DeckId { get; set; }

        public int NPlayers { get; set; }

        public int PlayerTurn { get; set; }

        public int DeckCardValue { get; set; }

        public string DeckCardJson { get; set; }

        public List<Card> Cards { get; set; }
    }
}