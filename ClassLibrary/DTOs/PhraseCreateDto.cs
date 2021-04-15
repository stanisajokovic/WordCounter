using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary.DTOs
{
    public class PhraseCreateDto
    {

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
    }
}
