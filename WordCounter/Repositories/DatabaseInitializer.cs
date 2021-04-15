using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Models;
using WordCounter.Repositories;

namespace WordCounterAPI.Repositories
{
    public static class DatabaseInitializer
    {
        public static void Initialize(PhraseDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            if (!dbContext.Phrases.Any())
            {
           
                dbContext.Phrases.Add(new Phrase() { Text = "test prvohg testa " });
                dbContext.Phrases.Add(new Phrase() { Text = "test prvohg texta " });
                dbContext.Phrases.Add(new Phrase() { Text = "Inicijalni podaci pri startupu " });

            }

        }
    }
}
