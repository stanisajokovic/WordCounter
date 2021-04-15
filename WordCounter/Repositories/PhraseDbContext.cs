using Microsoft.EntityFrameworkCore;
using WordCounter.Models;

namespace WordCounter.Repositories
{
    public class PhraseDbContext : DbContext
    {
        public PhraseDbContext(DbContextOptions<PhraseDbContext> opt) : base(opt)
        {
        }

        public DbSet<Phrase> Phrases { get; set; }
    }
}
