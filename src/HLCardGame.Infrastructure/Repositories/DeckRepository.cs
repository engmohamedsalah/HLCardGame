using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HLCardGame.Domain;
using HLCardGame.Infrastructure.DbContexts;
using HLCardGame.Infrastructure.Entities;
using HLCardGame.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HLCardGame.Infrastructure.Repositories
{
    public class DeckRepository : GenericRepository<DeckEntity, Deck>, IDeckRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public DeckRepository(HLCardGameDbContext dbContext)
            : base(dbContext, DeckMapper.ToEntity, DeckMapper.ToModel)
        {
        }

        /// <summary>
        /// Gets the by deck by identifier with cards asynchronous.
        /// </summary>
        /// <param name="deckId">The deck identifier.</param>
        /// <returns></returns>
        public async Task<Deck> GetByDeckByIdWithCardsAsync(Guid deckId)
        {
            var result = await _table
                .Include(a => a.Cards)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.DeckId == deckId);
            return result.ToModel();
        }

        public async Task<int> UpdateDeckCardAsync(Deck deck)
        {
            var entity = _table.Include(a => a.Cards).FirstOrDefault(a => a.DeckId == deck.DeckId);

            var model = deck.ToEntity();
            if (entity != null)
            {
                // Update parent
                DbContext.Entry(entity).CurrentValues.SetValues(model);

                DbContext.Cards.Remove(entity.Cards.FirstOrDefault());
            }
            return await DbContext.SaveChangesAsync();
        }
    }
}