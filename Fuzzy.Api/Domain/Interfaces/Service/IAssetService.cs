using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuzzy.Api.Domain.Interfaces.Service
{
    public interface IAssetService
    {
        Task PopulateAssets(int pageSize, int pageIndex = 0);
        Task PopulateAssetsForecast(int pageSize, int quantity);
        IEnumerable<AssetModel> GetAssets(int pageSize);
        IEnumerable<AssetForecastModel> GetAssetForecast(int valueToApply, int pageSize);
        Task<IEnumerable<AssetForecastModel>> GetAssetForecastAsync(int valueToApply, int pageSize);
        Task<AssetForecastModel> GetBitcoinAsync(int valueToApply);
        AssetForecast GetAssetForecastById(Guid id);
        Asset GetBySymbol(string symbol);
    }
}
