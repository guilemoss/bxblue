
using System;
using System.Collections.Generic;

namespace Fuzzy.Api.Domain.Entities
{
    public class Asset : Entity<Guid>
    {
        protected Asset() { }
        public Asset(string symbol, string name, string type = "Fund")
        {
            Symbol = symbol;
            Name = name;
            Type = type;
        }

        public string Symbol { get; }
        public string Type { get; }
        public string Name { get; }

        public virtual IList<WalletAsset> WalletAssets { get; }
        public virtual IList<AssetForecast> AssetsForecast { get; set; }

    }
}
