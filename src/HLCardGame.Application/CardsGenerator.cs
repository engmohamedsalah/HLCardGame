using System;
using System.Collections.Generic;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Helper;

namespace HLCardGame.Application
{
    public static class CardsGenerator
    {
        public static List<Card> CreateCards()
        {
            List<Card> cards = new List<Card>();
            for (int i = 2; i <= 14; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Add(new Card(
                        cardId: Guid.NewGuid(),
                        suit: suit.ToString(),
                        value: i,
                        displayName: CardHelper.GetShortName(i, suit),
                        color: (suit == Suit.Diamonds || suit == Suit.Hearts) ?
                                        CardColor.Red.ToString() : CardColor.Black.ToString()
                    ));
                }
            }
            ShuffleCards(cards);
            return cards;
        }

        public static void ShuffleCards(List<Card> cards)
        {
            //Shuffle the existing cards using Fisher-Yates Modern

            Random r = new Random(DateTime.Now.Millisecond);
            for (int n = cards.Count - 1; n > 0; --n)
            {
                // Randomly pick a card which has not been shuffled
                int k = r.Next(n + 1);

                //Swap the selected item
                //with the last "unselected" card in the collection
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }
        }
    }
}