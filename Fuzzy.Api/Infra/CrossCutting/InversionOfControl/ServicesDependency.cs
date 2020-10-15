using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Infra.CrossCutting.RequestProvider;
using Fuzzy.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fuzzy.Api.Infra.CrossCutting.InversionOfControl
{
    public static class ServicesDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IRequestProvider, RequestProvider.RequestProvider>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IBitcoinService, BitcoinService>();
            services.AddScoped<IWalletService, WalletService>();
        }
    }
}