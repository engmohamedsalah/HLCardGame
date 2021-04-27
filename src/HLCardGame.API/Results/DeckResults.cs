using System;

namespace HLCardGame.API.Results
{
    /// <summary>
    /// result of deck current info
    /// </summary>
    public class DeckResults
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckResults"/> class.
        /// </summary>
        /// <param name="deckId">The deck identifier.</param>
        /// <param name="shuffled">if set to <c>true</c> [shuffled].</param>
        /// <param name="playerTurn">The player turn.</param>
        /// <param name="nPlayer">The n player.</param>
        /// <param name="remaining">The remaining.</param>
        public DeckResults(
            Guid deckId,
            bool shuffled,
            int playerTurn,
            int nPlayer,
            int remaining)
        {
            DeckId = deckId;
            Shuffled = shuffled;
            PlayerTurn = playerTurn;
            NPlayer = nPlayer;

            Remaining = remaining;
        }

        public Guid DeckId { get; }

        public bool Shuffled { get; }

        public int PlayerTurn { get; }

        public int NPlayer { get; }

        public int Remaining { get; }
    }
}