using System;
using System.Collections.Generic;

namespace Fuzzy.Api.Domain.Models
{
    public class WalletInvestimentModel
    {
        public WalletInvestimentModel(string name)
        {
            Name = name;
        }

        public WalletInvestimentModel(Guid id, string name, IEnumerable<WalletAssetModel> walletAssets)
        {
            Id = id;
            Name = name;
            WalletAssets = walletAssets;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<WalletAssetModel> WalletAssets { get; set; }
    }
}
