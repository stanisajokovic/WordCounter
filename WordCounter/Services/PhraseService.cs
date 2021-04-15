using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Repositories;
using DTOsLibrary.DTOs;
using WordCounter.Models;
using WordCounterAPI.Repositories.UnitOfWork;
using System.Text.RegularExpressions;

namespace WordCounterAPI.Services
{
    public class PhraseService : IPhraseService
    {
        private readonly IMapper _mapper;
        private readonly IPhraseRepo _repository;
        private readonly IUnitOfWork _unitOfWork;
   
        public PhraseService(IMapper mapper, IPhraseRepo repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PhraseReadDto>> GetAllPhrases()
        {
            var phraseItems = _repository.GetAllPhrases();

            return _mapper.Map<IEnumerable<PhraseReadDto>>(phraseItems);
        }

        public async Task<PhraseReadDto> GetPhraseById(int id)
        {
            var phraseItem = _repository.GetPhraseById(id);
            if (phraseItem == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<PhraseReadDto>(phraseItem); ;
        }

        public async Task<PhraseReadDto> CreatePhrase(PhraseCreateDto phraseCreateDto)
        {
            var phraseItem = _mapper.Map<Phrase>(phraseCreateDto);
            if (phraseItem == null)
            {
                throw new ArgumentNullException(nameof(phraseItem));
            }
            using (var sqlTxn = _unitOfWork.Begin())
            {
                _repository.CreatePhrase(phraseItem);
                _repository.SaveChanges();

                sqlTxn.Commit();
            }

            return _mapper.Map<PhraseReadDto>(phraseItem);
        }

        public async Task DeletePhrase(int id)
        {
            var phraseModelFromRepo = _repository.GetPhraseById(id);
            if (phraseModelFromRepo == null)
            {
                throw new KeyNotFoundException();
            }

            using (var sqlTxn = _unitOfWork.Begin())
            {
                _repository.DeletePhrase(phraseModelFromRepo);
                _repository.SaveChanges();

                sqlTxn.Commit();
            }
        }

        public async Task<int> GetWordCount(string phrase)
        {
            if (!string.IsNullOrEmpty(phrase))
            {
                return Regex.Matches(phrase, @"[A-Za-z0-9]+").Count;
            }
            else
                throw new ArgumentNullException(nameof(phrase));
           
        }
    }
}
