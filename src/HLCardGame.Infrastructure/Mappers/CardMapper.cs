using System;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Helper;

namespace HLCardGame.Infrastructure.Mappers
{
    public static class CardMapper
    {
        public static Card ToModel(this CardEntity card)
        {
            if (card is null)
            {
                return default;
            }

            return new Card(
                cardId: card.CardId,
                color: card.Color.ToString(),
                suit: card.Suit.ToString(),
                value: card.Value,
                displayName: card.DisplayName);
        }

        public static CardEntity ToEntity(this Card card)
        {
            if (card is null)
            {
                return default;
            }

            return new CardEntity()
            {
                CardId = card.CardId,
                Color = Enum.Parse<CardColor>(card.Color),
                Suit = Enum.Parse<Suit>(card.Suit),
                Value = card.Value,
                DisplayName = card.DisplayName
            };
        }
    }
}