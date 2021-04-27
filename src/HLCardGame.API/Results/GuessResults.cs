using System;

namespace HLCardGame.API.Results
{
    /// <summary>
    ///
    /// </summary>
    public class GuessResults
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuessResults"/> class.
        /// </summary>
        /// <param name="sucess">if set to <c>true</c> [sucess].</param>
        /// <param name="card">The card.</param>
        public GuessResults(
            bool sucess,
            CardResults card,
            CardResults deckCard)
        {
            Card = card;
            Sucess = sucess;
            DeckCard = deckCard;
        }

        public bool Sucess { get; }

        public CardResults DeckCard { get; }

        public CardResults Card { get; }
    }
}