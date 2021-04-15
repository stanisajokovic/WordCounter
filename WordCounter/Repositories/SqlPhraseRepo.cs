using System;
using System.Collections.Generic;
using System.Linq;
using WordCounter.Models;

namespace WordCounter.Repositories
{
    public class SqlPhraseRepo : IPhraseRepo
    {
        private readonly PhraseDbContext _context;

        public SqlPhraseRepo(PhraseDbContext context)
        {
            _context = context;
        }

        public Phrase GetPhraseById(int Id)
        {
            return _context.Phrases.FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Phrase> GetAllPhrases()
        {
            return _context.Phrases.ToList();
        }

        public bool SaveChanges()
        {
           return( _context.SaveChanges() >= 0);
        }

        public void CreatePhrase(Phrase phrase)
        {
            _context.Phrases.Add(phrase);
        }

        public void DeletePhrase(Phrase phrase)
        {
            _context.Phrases.Remove(phrase);
        }
    }
}
