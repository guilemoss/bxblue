using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fuzzy.Api.Domain.Entities;

namespace Fuzzy.Api.Infra.Mapping
{
    public class AssetForecastMap : IEntityTypeConfiguration<AssetForecast>
    {
        public void Configure(EntityTypeBuilder<AssetForecast> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Date)
                .IsRequired();

            builder.Property(prop => prop.Price)
                .IsRequired();

            builder
                .HasOne(prop => prop.Asset)
                .WithMany(prop => prop.AssetsForecast)
                .HasForeignKey(prop => prop.AssetId);

        }
    }
}
