using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Domain.Models;
using Fuzzy.Api.Infra.CrossCutting.RequestProvider;
using System;
using System.Threading.Tasks;

namespace Fuzzy.Api.Infra.Repositories
{
    public class BitcoinRepository : IBitcoinRepository
    {
        private const string API_URL_BASE = "https://blockchain.info/tobtc?currency=USD";
        private readonly IRequestProvider _requestProvider;

        public BitcoinRepository(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<AssetForecastModel> GetBitcoinAsync(int valueToApply)
        {
            var uri = $"{API_URL_BASE}&value={valueToApply}";

            AssetForecastModel assetForecast;

            try
            {
                decimal valueInBTC = await _requestProvider.GetAsync<decimal>(uri);
                assetForecast = new AssetForecastModel(
                    Guid.NewGuid(), 
                    DateTime.Now,
                    valueInBTC,
                    valueToApply,
                    new AssetModel(Guid.NewGuid(), "BTC", "Bitcoin", "Crypto Currency")
                );
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                assetForecast = null;
            }

            return assetForecast;
        }
    }
}
