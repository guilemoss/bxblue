using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fuzzy.Api.Domain.Entities;

namespace Fuzzy.Api.Infra.Mapping
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .IsRequired();

            builder
                .HasMany(prop => prop.WalletAssets)
                .WithOne();
        }
    }
}
