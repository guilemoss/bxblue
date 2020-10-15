using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Domain.Models;
using Fuzzy.Api.Infra.CrossCutting.RequestProvider;
using Fuzzy.Api.Infra.CrossCutting.RequestProvider.MarketStack.Models;
using Fuzzy.Api.Shared.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuzzy.Api.Services
{
    public class AssetService : IAssetService
    {
        private const string ACCESS_KEY = "e72b199519fa05aeff3d1f063a3fb43f";
        private const string API_URL_BASE = "http://api.marketstack.com/v1";

        private readonly IRequestProvider _requestProvider;
        private readonly IBitcoinService _bitcoinService;
        private readonly IAssetRepository _assetRepository;
        public AssetService(IRequestProvider requestProvider, IBitcoinService bitcoinService, IAssetRepository assetRepository)
        {
            _requestProvider = requestProvider;
            _bitcoinService = bitcoinService;
            _assetRepository = assetRepository;
        }
        
        public async Task PopulateAssets(int pageSize, int pageIndex = 0) 
        {
            var assets = await GetProviderAssets(pageSize, pageIndex);
            await _assetRepository.Populate(assets);
        }

        public async Task PopulateAssetsForecast(int pageSize, int quantity)
        {
            var assetsForecast = await GetProviderAssetsForecast(pageSize, quantity);
            _assetRepository.PopulateAssetsForecast(assetsForecast);
        }

        public async Task<AssetForecastModel> GetBitcoinAsync(int valueToApply)
        {
            return (await _bitcoinService.GetBitcoinAsync(valueToApply));
        }

        private async Task<IEnumerable<AssetForecast>> GetProviderAssetsForecast(int pageSize, int quantity)
        {
            IList<AssetForecast> assetsForecast;

            try
            {
                var assets = GetAssets(pageSize);
                var arrSymbol = assets.Select(q => q.Symbol).ToArray();
                var quantityFunds = arrSymbol.Length;
                var symbols = string.Join(",", arrSymbol);
                var limit = (quantityFunds * quantity);
                var uri = $"{API_URL_BASE}/intraday?limit={limit}&access_key={ACCESS_KEY}&symbols={symbols}";

                var response = await _requestProvider.GetAsync<IntradayModel>(uri);
                assetsForecast = response.Data
                    .GroupBy(q => q.Symbol)
                    .OrderByDescending(o => o.Take(quantity).Average(a => a.Close))
                    .Select(s => new AssetForecast(assets.FirstOrDefault(q => q.Symbol == s.Key).Id, s.FirstOrDefault().Close))
                    .ToList();
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                assetsForecast = null;
            }

            return assetsForecast;
        }

        private async Task<IEnumerable<Asset>> GetProviderAssets(int pageSize, int pageIndex)
        {
            var offset = pageSize * pageIndex;
            var uri = $"{API_URL_BASE}/tickers?limit={pageSize}&offset={offset}&access_key={ACCESS_KEY}";

            try
            {
                var response = await _requestProvider.GetAsync<TickerModel>(uri);
                var assets = response.Data.Select(s => new Asset(s.Symbol, s.Name));
                return assets;
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public IEnumerable<AssetModel> GetAssets(int pageSize)
        {
            return _assetRepository.GetAll(pageSize)
                .ConvertToAssetsModel();
        }

        public IEnumerable<AssetForecastModel> GetAssetForecast(int valueToApply, int pageSize) 
        {
            var assetBitcoin = GetBitcoinAsync(valueToApply).Result;
            var assetsForecast = _assetRepository.GetForecast(pageSize);
            var data = (IList<AssetForecastModel>)assetsForecast
                .ConvertToAssetsForecastModel(valueToApply);
            data.Add(assetBitcoin);

            return data;
        }

        public async Task<IEnumerable<AssetForecastModel>> GetAssetForecastAsync(int valueToApply, int pageSize)
        {
            var assetBitcoin = await GetBitcoinAsync(valueToApply);
            var assetsForecast = (await _assetRepository.GetForecastAsync(pageSize));
            var data = (IList<AssetForecastModel>)assetsForecast.ConvertToAssetsForecastModel(valueToApply);
            data.Add(assetBitcoin);
            
            return data;
        }

        public AssetForecast GetAssetForecastById(Guid id)
        {
            return _assetRepository.GetAssetForecastById(id);
                //.ConvertToAssetForecastModel(valueToApply);
        }

        public Asset GetBySymbol(string symbol) =>
            _assetRepository.GetBySymbol(symbol);

    }
}