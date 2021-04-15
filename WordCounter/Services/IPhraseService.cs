using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.DTOs;

namespace WordCounterAPI.Services
{
    public interface IPhraseService
    {
        Task<PhraseReadDto> CreatePhrase(PhraseCreateDto phraseCreateDto);
        Task DeletePhrase(int id);
        Task<IEnumerable<PhraseReadDto>> GetAllPhrases();
        Task<PhraseReadDto> GetPhraseById(int id);
        Task<int> GetWordCount(string phrase);
    }
}