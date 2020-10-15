using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fuzzy.Api.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity, TKeyType>
    {
        void Save(TEntity obj);

        void Remove(TKeyType id);

        TEntity GetById(TKeyType id);

        IList<TEntity> GetAll();
        IList<TEntity> GetAll(int pageSize);

        Task Populate(IEnumerable<TEntity> entities);
        //IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query,
        //Expression<Func<TEntity, object>> expression);
        //Expression<Func<TEntity, bool>> filter,
        IQueryable<TEntity> GetEagerLoad(params Expression<Func<TEntity, object>>[] children);

    }
}

