using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Helper;

namespace HLCardGame.Application
{
    public interface IDeckService
    {
        Task<Deck> CreateDeckAsync(int nPlayer);

        Task<Card> DrawCardAsync(Guid deckId);

        Task<(bool, Card, Card)> GuessNextCardAsync(Guid deckId, GuessDirection directionGuess);

        Task<int> CancelDeckAsync(Guid deckId);

        Task<Deck> GetDeckIdByIdAsync(Guid deckId);
    }
}