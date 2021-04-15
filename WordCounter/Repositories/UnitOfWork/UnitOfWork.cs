using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Repositories;

namespace WordCounterAPI.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhraseDbContext _context;

        public UnitOfWork(PhraseDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction Begin()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
