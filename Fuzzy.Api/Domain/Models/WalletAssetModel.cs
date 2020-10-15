using System;

namespace Fuzzy.Api.Domain.Models
{
    public class WalletAssetModel
    {
        public WalletAssetModel() { }

        public WalletAssetModel(Guid id, Guid walletId, DateTime date, int value, decimal price, AssetModel asset)
        {
            Id = id;
            Date = date;
            Value = value;
            Price = price;
            Asset = asset;
            AssetId = asset.Id;
            WalletId = walletId;
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Value { get; set; }

        public Guid WalletId { get; set; }
        public Guid AssetId { get; set; }
        public AssetModel Asset { get; set; }

    }
}
