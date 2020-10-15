using Fuzzy.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fuzzy.Api.Infra.Mapping
{
    public class AssetMap : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .IsRequired();

            builder.Property(prop => prop.Symbol)
                .IsRequired();

            builder.Property(prop => prop.Type)
                .IsRequired();

            builder
                .HasMany(prop => prop.WalletAssets)
                .WithOne();

            builder
                .HasMany(prop => prop.AssetsForecast)
                .WithOne();
        }
    }
}
