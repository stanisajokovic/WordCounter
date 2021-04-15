using System.Collections.Generic;
using WordCounter.Models;

namespace WordCounter.Repositories
{
    public class TestPhraseRepo : IPhraseRepo
    {

        public Phrase GetPhraseById(int Id)
        {
            return new Phrase() { Id = 0, Text = "Tata voli tita"};
        }

        public IEnumerable<Phrase> GetAllPhrases()
        {
            var phrases = new List<Phrase>
            {
                new Phrase(){Id = 0, Text = "test prvohg testa "},
                new Phrase(){Id = 0, Text = "proba "},
                new Phrase(){Id = 0, Text = " program boji reci u textu"}
            };
            return phrases;
            
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void CreatePhrase(Phrase phrase)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePhrase(Phrase phrase)
        {
            throw new System.NotImplementedException();
        }
    }
}
