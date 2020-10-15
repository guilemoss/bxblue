using System;

namespace Fuzzy.Api.Domain.Models.WalletsModel
{
    public class PurchaseWalletAssetModel
    {
        public Guid WalletId { get; set; }
        public Guid AssetForecastId { get; set; }
        
        public int Value { get; set; }

    }
}
