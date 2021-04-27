using System;
using HLCardGame.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HLCardGame.Infrastructure.Tests.Repositories
{
    public class InMemoryDbContext
    {
        public static HLCardGameDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<HLCardGameDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
            var dbContext = new HLCardGameDbContext(options);
            return dbContext;
        }
    }
}