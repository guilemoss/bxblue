using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Domain.Models;
using Fuzzy.Api.Domain.Models.WalletsModel;
using Fuzzy.Api.Shared.Mapper;
using System;
using System.Collections.Generic;

namespace Fuzzy.Api.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IAssetService _assetService;
        public WalletService(IWalletRepository walletRepository, IAssetService assetService)
        {
            _walletRepository = walletRepository;
            _assetService = assetService;
        }

        public IEnumerable<WalletInvestimentModel> GetAll()
        {
            var wallets = _walletRepository.GetAll();
            return wallets.ConvertToWalletInvestimentsModel();
        }

        public IEnumerable<WalletModel> GetCombo()
        {
            var wallets = _walletRepository.GetAll();
            return wallets.ConvertToWalletsModel();
        }
        
        public WalletInvestimentModel GetById(Guid id)
        {
            var wallet = _walletRepository.GetById(id);
            return wallet.ConvertToWalletInvestimentModel();
        }

        public WalletAssetModel PurchaseWalletAsset(PurchaseWalletAssetModel walletAssetModel) 
        {
            var wallet = _walletRepository.GetById(walletAssetModel.WalletId);
            var assetForecast = _assetService.GetAssetForecastById(walletAssetModel.AssetForecastId);

            if (assetForecast == null) {
                var asset = _assetService.GetBySymbol("BTC");
                var bitcoin = _assetService.GetBitcoinAsync(walletAssetModel.Value).Result;
                assetForecast = new AssetForecast(asset.Id, bitcoin.Price);
            }
            
            var walleAsset = new WalletAsset(walletAssetModel.WalletId, assetForecast.AssetId, walletAssetModel.Value, assetForecast.Price);
            wallet.WalletAssets.Add(walleAsset);
            _walletRepository.Save(wallet);

            return walleAsset.ConvertToWalletAssetModel();
        }

        public WalletModel Register(CreateWalletModel walletModel)
        {
            var wallet = walletModel.ConvertToWalletEntity();
            _walletRepository.Save(wallet);
            return wallet.ConvertToWalletModel();
        }
    }
}
