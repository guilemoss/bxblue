using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fuzzy.Api.Infra.CrossCutting.InversionOfControl
{
    public static class SqlRepositoryDependency
    {
        public static void AddSqlRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IBitcoinRepository, BitcoinRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
        }
    }
}
