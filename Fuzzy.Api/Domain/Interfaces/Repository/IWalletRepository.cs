using System;
using System.Collections.Generic;
using Fuzzy.Api.Domain.Entities;

namespace Fuzzy.Api.Domain.Interfaces.Repository
{
    public interface IWalletRepository
    {
        void Save(Wallet obj);

        void Remove(Guid id);

        Wallet GetById(Guid id);

        IList<Wallet> GetAll();
    }
}

