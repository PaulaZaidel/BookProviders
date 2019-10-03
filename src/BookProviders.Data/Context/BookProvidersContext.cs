
using BookProviders.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookProviders.Data.Context
{
    public class BookProvidersContext : DbContext
    {
        public BookProvidersContext(DbContextOptions<BookProvidersContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Caterer> Caterers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure mvarchar to varchar
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";

            // Configure Mappings
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookProvidersContext).Assembly);

            // Turn Off delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
