using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Tests.Utilities
{
    public interface IInMemoryDatabaseFactory<TContext> where TContext : DbContext
    {
        TContext Create(string databaseName);
    }
}
