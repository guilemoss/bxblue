using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Fuzzy.Api
{
    public static class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = typeof(Program).Assembly.GetName().Name;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<DataContext>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            var serviceScopeFactory = (IServiceScopeFactory)host
                .Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<DataContext>();
                var assetsService = services.GetService<IAssetService>();
                AddDataDefault(context);
                PopulateForecast(assetsService).Wait();
                //AddMock(context);
            }

            return host;
        }

        private static void AddDataDefault(DataContext context)
        {
            var defaultWallet = new Domain.Entities.Wallet("Default");
            context.Wallets.Add(defaultWallet);

            var assetBitcoin = new Domain.Entities.Asset("BTC", "Bitcoin", "Crypto Currency");
            context.Assets.Add(assetBitcoin);
            context.SaveChanges();
        }

        private static void AddMock(DataContext context)
        {
            var asset1 = new Domain.Entities.Asset("SIRI", "Sirius XM Holdings Inc");
            var asset2 = new Domain.Entities.Asset("ERIC", "Ericsson ADR");
            var asset3 = new Domain.Entities.Asset("VOD", "Vodafone Group Public Limited Company");
            
            context.Assets.Add(asset1);
            context.Assets.Add(asset2);
            context.Assets.Add(asset3);
            
            var defaultWallet = new Domain.Entities.Wallet("Default");
            defaultWallet.WalletAssets.Add(new Domain.Entities.WalletAsset(defaultWallet.Id, asset1.Id, 300, 10.40M));
            defaultWallet.WalletAssets.Add(new Domain.Entities.WalletAsset(defaultWallet.Id, asset2.Id, 600, 4.50M));
            context.Wallets.Add(defaultWallet);

            context.AssetsForecast.Add(new Domain.Entities.AssetForecast(asset1.Id, 5.84M));
            context.AssetsForecast.Add(new Domain.Entities.AssetForecast(asset2.Id, 10.87M));
            context.AssetsForecast.Add(new Domain.Entities.AssetForecast(asset3.Id, 14.63M));

            context.SaveChanges();
        }

        private static async System.Threading.Tasks.Task PopulateForecast(IAssetService assetService)
        {
            const int pageSize = 20;
            const int quantityLastDays = 30;
            await assetService.PopulateAssets(pageSize);
            await assetService.PopulateAssetsForecast(pageSize, quantityLastDays);
        }
    }
}
