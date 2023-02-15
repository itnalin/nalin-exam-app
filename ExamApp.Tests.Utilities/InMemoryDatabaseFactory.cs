using Microsoft.EntityFrameworkCore;
using System;

namespace ExamApp.Tests.Utilities
{
    public class InMemoryDatabaseFactory<TContext> : IInMemoryDatabaseFactory<TContext>
        where TContext : DbContext
    {
        public TContext Create(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseInMemoryDatabase(databaseName: databaseName);

            var _dbContext = (TContext)Activator.CreateInstance(typeof(TContext), builder.Options);
            // Delete existing db before creating a new one

            if(_dbContext != null)
            {
                _dbContext.Database.EnsureDeleted();
                _dbContext.Database.EnsureCreated();
            }            

            return _dbContext;
        }
    }
}
