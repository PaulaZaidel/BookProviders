
using BookProviders.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace BookProviders.Data.Context
{
    class BookProvidersContext : DbContext
    {
        public BookProvidersContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Caterer> Caterers { get; set; }
        public DbSet<Address> Adresses { get; set; }
    }
}
