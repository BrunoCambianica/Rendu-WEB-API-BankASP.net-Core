using Microsoft.EntityFrameworkCore;
using BankModels;

namespace Bank.BankDbContext
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Movement> Movements { get; set; }

    }
}
