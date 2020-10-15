using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Models;
using Fuzzy.Api.Domain.Models.WalletsModel;
using System;

namespace Fuzzy.Api.Shared.Mapper
{
    public static class WalletAssetMapper
    {
        public static WalletAsset ConvertToWalletAssetEntity(this PurchaseWalletAssetModel walletAssetModel, Guid assetId, decimal AssetPrice) =>
            new WalletAsset(walletAssetModel.WalletId, assetId, walletAssetModel.Value, AssetPrice);
        public static WalletAssetModel ConvertToWalletAssetModel(this WalletAsset walletAsset) =>
            new WalletAssetModel(walletAsset.WalletId, walletAsset.AssetId, walletAsset.InvestimentDate, walletAsset.Value, walletAsset.Price, new AssetModel(walletAsset.AssetId, walletAsset.Asset?.Symbol, walletAsset.Asset?.Name, walletAsset.Asset?.Type));
    }
}
