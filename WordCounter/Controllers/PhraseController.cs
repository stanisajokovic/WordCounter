using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using WordCounterAPI.Services;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.RegularExpressions;

namespace WordCounter.Controllers
{
    [Authorize]
    [Route("api/wordcounter")]
    [ApiController]
    public class PhraseController : ControllerBase
    {
        private IPhraseService _phraseService;

        public PhraseController(IPhraseService phraseService)
        {
            _phraseService = phraseService;
        }
        //[Authorize]
        [HttpGet(Name = "GetAllPhrases")]
        [SwaggerOperation(OperationId = "GetAllPhrases")]
        public async Task<ActionResult<PhraseReadDto>> GetAllPhrases()
        {
            var result = await _phraseService.GetAllPhrases();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetPhraseById")]
        [SwaggerOperation(OperationId = "GetPhraseById")]
        public async Task<ActionResult<PhraseReadDto>> GetPhraseById(int id)
        {
            try
            {
                var phraseItem = await _phraseService.GetPhraseById(id);
                return Ok(phraseItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "CreatePhrase")]
        [SwaggerOperation(OperationId = "CreatePhrase")]
        public async Task<ActionResult<PhraseReadDto>> CreatePhrase([FromBody][Required] PhraseCreateDto phraseCreateDto)
        {
            try
            {
                var createdPhrase = await _phraseService.CreatePhrase(phraseCreateDto);

                return CreatedAtRoute(nameof(GetPhraseById), new { Id = createdPhrase.Id }, createdPhrase);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}", Name = "DeletePhrase")]
        [SwaggerOperation(OperationId = "DeletePhrase")]
        public async Task<ActionResult> DeletePhrase(int id)
        {
            try
            {
                await _phraseService.DeletePhrase(id);
                return NoContent();

            }
            catch (KeyNotFoundException)
            {

                return NotFound();
            }

        }

        [HttpPost("count", Name = "PostWordCount")]
        [SwaggerOperation(OperationId = "GetWordCount")]
        public async Task<ActionResult<int>> GetWordCount([FromBody][Required] string text)
        {
            Regex rx = new Regex(@"\b\S+\b");
            MatchCollection matches = rx.Matches(text);
            return matches.Count;
        }
    }
}
