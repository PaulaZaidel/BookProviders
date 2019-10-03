
using BookProviders.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookProviders.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasColumnType("varchar(400)");

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(a => a.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Addresses");
        }
    }
}
