using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.Entities;

namespace HLCardGame.Infrastructure.Repositories
{
    public interface IDeckRepository : IGenericRepository<DeckEntity, Deck>
    {
        Task<Deck> GetByDeckByIdWithCardsAsync(Guid deckId);

        Task<int> UpdateDeckCardAsync(Deck deck);
    }
}