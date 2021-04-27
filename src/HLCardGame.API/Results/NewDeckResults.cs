using System;

namespace HLCardGame.API.Results
{
    public class NewDeckResults
    {
        public NewDeckResults(
            Guid deckId,
            int remaining,
            CardResults card)
        {
            DeckId = deckId;
            Card = card;
            Remaining = remaining;
        }

        public Guid DeckId { get; }

        public int Remaining { get; }

        public CardResults Card { get; }
    }
}