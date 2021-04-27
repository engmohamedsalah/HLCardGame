namespace HLCardGame.API.Results
{
    /// <summary>
    /// return card result
    /// </summary>
    public class CardResults
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardResults"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="suit">The suit.</param>
        /// <param name="value">The value.</param>
        /// <param name="displayName">The display name.</param>
        public CardResults(
           string color,
           string suit,
           int value,
           string displayName)
        {
            Color = color;
            Suit = suit;
            Value = value;
            DisplayName = displayName;
        }

        public string Color { get; }

        public string Suit { get; }

        public int Value { get; }

        public string DisplayName { get; }
    }
}