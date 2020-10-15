using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fuzzy.Api.Infra.Repositories
{
    public class AssetRepository : Repository<Asset, Guid>, IAssetRepository
    {
        public AssetRepository(DataContext dataContext) : base(dataContext)
        {
        }
        
        public void Remove(Guid id) =>
           base.Delete(id);
        public void Save(Asset obj)
        {
            if (obj.Id == Guid.Empty)
                base.Insert(obj);
            else
                base.Update(obj);
        }

        public Asset GetById(Guid id) =>
            base.Select(id);

        public IList<Asset> GetAll() =>
            base.Select();

        public IList<Asset> GetAll(int pageSize) =>
            base.Select(pageSize);

        public async Task Populate(IEnumerable<Asset> entities) => 
            await base.InsertAll(entities);

        public void PopulateAssetsForecast(IEnumerable<AssetForecast> entities)
        {
            _dataContext.AssetsForecast.AddRange(entities);
            _dataContext.SaveChanges();
        }
        public void AddAssetForecast(AssetForecast assetForecast)
        {
            _dataContext.AssetsForecast.Add(assetForecast);
            _dataContext.SaveChanges();
        }

        public IList<AssetForecast> GetForecast(int pageSize)
        {
            return _dataContext.AssetsForecast
                    .Include(inc => inc.Asset)
                    .Take(pageSize)
                    .ToList();
        }

        public async Task<IList<AssetForecast>> GetForecastAsync(int pageSize)
        {
            return await _dataContext.AssetsForecast
                    .AsQueryable()
                    .Include(inc => inc.Asset)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public AssetForecast GetAssetForecastById(Guid id) =>
            _dataContext.AssetsForecast
            .Include(inc => inc.Asset)
            .FirstOrDefault(filter => filter.Id == id);

        public Asset GetBySymbol(string symbol) =>
            _dataContext.Assets.FirstOrDefault(f => f.Symbol == symbol);

        IQueryable<Asset> IRepository<Asset, Guid>.GetEagerLoad(params Expression<Func<Asset, object>>[] children) 
            => base.GetEagerLoad(children);
    }
}
