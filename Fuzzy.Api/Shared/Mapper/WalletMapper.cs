using System.Collections.Generic;
using System.Linq;
using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Models;
using Fuzzy.Api.Domain.Models.WalletsModel;

namespace Fuzzy.Api.Shared.Mapper
{
    public static class WalletMapper
    {
        public static WalletInvestimentModel ConvertToWalletInvestimentModel(this Wallet wallet) =>
            new WalletInvestimentModel(wallet.Id, wallet.Name, 
                wallet.WalletAssets?.Select(s => new WalletAssetModel(s.Id, s.WalletId, s.InvestimentDate, s.Value, s.Price,
                        new AssetModel(s.Id, s.Asset.Symbol, s.Asset.Name, s.Asset.Type))));

        public static IEnumerable<WalletInvestimentModel> ConvertToWalletInvestimentsModel(this IList<Wallet> wallets) =>
            new List<WalletInvestimentModel>(wallets.Select(wallet => ConvertToWalletInvestimentModel(wallet)));

        public static WalletModel ConvertToWalletModel(this Wallet wallet) =>
            new WalletModel(wallet.Id, wallet.Name);

        public static IEnumerable<WalletModel> ConvertToWalletsModel(this IList<Wallet> wallets) =>
            new List<WalletModel>(wallets.Select(wallet => ConvertToWalletModel(wallet)));

        public static Wallet ConvertToWalletEntity(this CreateWalletModel walletModel) =>
            new Wallet(walletModel.Name);
    }
}
