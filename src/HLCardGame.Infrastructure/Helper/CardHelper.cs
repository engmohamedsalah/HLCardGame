using System;
using HLCardGame.Infrastructure.Entities;

namespace HLCardGame.Infrastructure.Helper
{
    public static class CardHelper
    {
        public static string GetShortName(int value, Suit suit)
        {
            string valueDisplay = "";
            if (value >= 2 && value <= 10)
            {
                valueDisplay = value.ToString();
            }
            else if (value == 11)
            {
                valueDisplay = "J";
            }
            else if (value == 12)
            {
                valueDisplay = "Q";
            }
            else if (value == 13)
            {
                valueDisplay = "K";
            }
            else if (value == 14)
            {
                valueDisplay = "A";
            }

            return valueDisplay + Enum.GetName(typeof(Suit), suit)[0];
        }
    }
}