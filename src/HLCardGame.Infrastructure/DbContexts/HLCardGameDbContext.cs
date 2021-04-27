using HLCardGame.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HLCardGame.Infrastructure.DbContexts
{
    public class HLCardGameDbContext : DbContext
    {
        public HLCardGameDbContext(DbContextOptions<HLCardGameDbContext> dbContextOptionsBuilder)
            : base(dbContextOptionsBuilder)
        {
        }

        public virtual DbSet<CardEntity> Cards { get; set; }
        public virtual DbSet<DeckEntity> Games { get; set; }
    }
}