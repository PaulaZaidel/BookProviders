
using BookProviders.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookProviders.Data.Mappings
{
    public class CatererMapping : IEntityTypeConfiguration<Caterer>
    {
        public void Configure(EntityTypeBuilder<Caterer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            //1:1 => Caterer : Address
            builder.HasOne(c => c.Address)
                .WithOne(a => a.Caterer);

            //1:N => Caterer : Products
            builder.HasMany(c => c.Products)
                .WithOne(p => p.Caterer)
                .HasForeignKey(p => p.CatererId);

            builder.ToTable("Caterers");
        }
    }
}
