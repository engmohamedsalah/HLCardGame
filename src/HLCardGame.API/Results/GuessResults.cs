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
        /// <param name="newCard">The card.</param>
        public GuessResults(
            bool sucess,
            CardResults newCard,
            CardResults oldCard)
        {
            NewCard = newCard;
            Sucess = sucess;
            OldCard = oldCard;
        }

        public bool Sucess { get; }

        public CardResults OldCard { get; }

        public CardResults NewCard { get; }
    }
}