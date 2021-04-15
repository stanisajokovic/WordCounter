using System;
using System.ComponentModel.DataAnnotations;

namespace WordCounter.Models
{
    public class Phrase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
    }
}
