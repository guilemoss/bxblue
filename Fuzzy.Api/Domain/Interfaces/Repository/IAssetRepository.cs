using Fuzzy.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuzzy.Api.Domain.Interfaces.Repository
{
    public interface IAssetRepository : IRepository<Asset, Guid>
    {
        IList<AssetForecast> GetForecast(int pageSize);
        Task<IList<AssetForecast>> GetForecastAsync(int pageSize);
        void PopulateAssetsForecast(IEnumerable<AssetForecast> entities);
        AssetForecast GetAssetForecastById(Guid id);
        void AddAssetForecast(AssetForecast assetForecast);
        Asset GetBySymbol(string symbol);
    }
}
