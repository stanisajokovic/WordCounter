using System.Collections.Generic;
using WordCounter.Models;

namespace WordCounter.Repositories
{
    public interface IPhraseRepo
    {
        bool SaveChanges();

        IEnumerable<Phrase> GetAllPhrases();
        Phrase GetPhraseById(int Id);
        void CreatePhrase(Phrase phrase);

        void DeletePhrase(Phrase phrase);

    }
}
