using System.Collections.Generic;
using System.Linq;
using Fuzzy.Api.Infra.Context;
using Fuzzy.Api.Domain.Entities;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace Fuzzy.Api.Infra.Repositories
{
    public class Repository<TEntity, TKeyType> where TEntity : Entity<TKeyType>
    {
        protected readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected virtual void Insert(TEntity obj)
        {
            _dataContext.Set<TEntity>().Add(obj);
            _dataContext.SaveChanges();
        }

        protected virtual void Update(TEntity obj)
        {
            _dataContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dataContext.SaveChanges();
        }

        protected virtual void Delete(TKeyType id)
        {
            _dataContext.Set<TEntity>().Remove(Select(id));
            _dataContext.SaveChanges();
        }

        protected virtual IList<TEntity> Select() =>
            _dataContext.Set<TEntity>().ToList();

        protected virtual IList<TEntity> Select(int pageSize) =>
            _dataContext.Set<TEntity>().Take(pageSize).ToList();

        protected virtual TEntity Select(TKeyType id) =>
            _dataContext.Set<TEntity>().Find(id);

        protected virtual async Task InsertAll(IEnumerable<TEntity> entities)
        {
            await _dataContext.Set<TEntity>().AddRangeAsync(entities);
            await _dataContext.SaveChangesAsync();
        }
        protected virtual IQueryable<TEntity> GetEagerLoad(params Expression<Func<TEntity, object>>[] children)
        {
            var _DbSet = _dataContext.Set<TEntity>();
            children.ToList().ForEach(entity => _DbSet.Include(entity).Load());
            return _DbSet;
        }
    }
}
