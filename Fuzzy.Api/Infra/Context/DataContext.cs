using Fuzzy.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Fuzzy.Api.Infra.Mapping;

namespace Fuzzy.Api.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetForecast> AssetsForecast { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletAsset> WalletAssets { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>(new AssetMap().Configure);
            modelBuilder.Entity<AssetForecast>(new AssetForecastMap().Configure);
            modelBuilder.Entity<Wallet>(new WalletMap().Configure);
            modelBuilder.Entity<WalletAsset>(new WalletAssetMap().Configure);
        }

    }
    
}
