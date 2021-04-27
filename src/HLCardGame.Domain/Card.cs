using System;

namespace HLCardGame.Domain
{
    public class Card
    {
        public Card
            (
            Guid cardId,
            string color,
            string suit,
            int value,
            string displayName)
        {
            CardId = cardId;
            Color = color;
            Suit = suit;
            Value = value;
            DisplayName = displayName;
        }

        public Guid CardId { get; }

        public string Color { get; }

        public string Suit { get; }

        public int Value { get; }

        public string DisplayName { get; }
    }
}