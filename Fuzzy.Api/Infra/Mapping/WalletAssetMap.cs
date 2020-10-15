using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fuzzy.Api.Domain.Entities;

namespace Fuzzy.Api.Infra.Mapping
{
    public class WalletAssetMap : IEntityTypeConfiguration<WalletAsset>
    {
        public void Configure(EntityTypeBuilder<WalletAsset> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.InvestimentDate)
                .IsRequired();

            builder.Property(prop => prop.Value)
                .IsRequired();

            builder
                .HasOne(prop => prop.Asset)
                .WithMany(prop => prop.WalletAssets)
                .HasForeignKey(prop => prop.AssetId);

            builder
                .HasOne(prop => prop.Wallet)
                .WithMany(prop => prop.WalletAssets)
                .HasForeignKey(prop => prop.WalletId);
        }
    }
}
