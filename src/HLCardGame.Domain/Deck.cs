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
            int lastCardValue,
            string deckCardJson,
            List<Card> cards)
        {
            DeckId = deckId;
            NPlayers = nPlayers;
            PlayerTurn = playerTurn;
            LastCardValue = lastCardValue;
            Cards = cards;
            LastCardJson = deckCardJson;
        }

        public Guid DeckId { get; set; }

        public int NPlayers { get; set; }

        public int PlayerTurn { get; set; }

        public int LastCardValue { get; set; }

        public string LastCardJson { get; set; }

        public List<Card> Cards { get; set; }
    }
}