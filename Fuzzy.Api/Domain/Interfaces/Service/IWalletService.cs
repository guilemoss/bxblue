using Fuzzy.Api.Domain.Models;
using Fuzzy.Api.Domain.Models.WalletsModel;
using System;
using System.Collections.Generic;

namespace Fuzzy.Api.Domain.Interfaces.Service
{
    public interface IWalletService
    {
        IEnumerable<WalletModel> GetCombo();
        IEnumerable<WalletInvestimentModel> GetAll();
        WalletInvestimentModel GetById(Guid id);
        WalletModel Register(CreateWalletModel walletModel);
        WalletAssetModel PurchaseWalletAsset(PurchaseWalletAssetModel walletAssetModel);
    }
}
