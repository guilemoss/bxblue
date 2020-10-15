using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Infra.Context;
using Fuzzy.Api.Shared.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzy.Api.Infra.Repositories
{
    public class WalletRepository : Repository<Wallet, Guid>, IWalletRepository
    {
        public WalletRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public void Remove(Guid id) =>
            base.Delete(id);

        public void Save(Wallet obj)
        {
            if (obj.Id == Guid.Empty)
                base.Insert(obj);
            else
                base.Update(obj);
        }

        public Wallet GetById(Guid id) =>
            _dataContext.Wallets
            .Include(inc => inc.WalletAssets)
            .ThenInclude(inc => inc.Asset)
            .FirstOrDefault(filter => filter.Id == id);

        public IList<Wallet> GetAll() =>
            _dataContext.Wallets
            .Include(inc => inc.WalletAssets)
            .ThenInclude(inc => inc.Asset)
            .ToList();
    }
}
