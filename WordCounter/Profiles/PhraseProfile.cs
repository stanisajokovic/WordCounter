using AutoMapper;
using ClassLibrary.DTOs;
using WordCounter.Models;

namespace WordCounter.Profiles
{
    public class PhraseProfile : Profile
    {
        public PhraseProfile()
        {
            CreateMap<Phrase, PhraseReadDto>();

            CreateMap<PhraseCreateDto, Phrase>();
        }
    }
}
