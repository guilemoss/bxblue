using System;

namespace Fuzzy.Api.Domain.Entities
{
    public class WalletAsset : Entity<Guid>
    {
        protected WalletAsset() {}

        public WalletAsset(Guid walletId, Guid assetId, int value, decimal price)
        {
            WalletId = walletId;
            AssetId = assetId;
            Value = value;
            Price = price;
            InvestimentDate = DateTime.Now;
        }

        public int Value { get; private set; }
        public decimal Price { get; private set; }
        public DateTime InvestimentDate{ get; private set; }

        public Guid AssetId { get; set; }
        public virtual Asset Asset { get; set; }

        public Guid WalletId { get; set; }
        public virtual Wallet Wallet { get; set; }

    }
}
