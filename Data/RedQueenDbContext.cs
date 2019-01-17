using Microsoft.EntityFrameworkCore;
using RedQueen.Models.Coins;

namespace RedQueen.Data
{
    public class RedQueenDbContext : DbContext
    {
        public RedQueenDbContext(DbContextOptions<RedQueenDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<Coin> Coins { get; set; }
    }
}
