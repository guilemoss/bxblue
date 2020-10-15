using System;
using System.Collections.Generic;

namespace Fuzzy.Api.Domain.Entities
{
    public class Wallet : Entity<Guid>
    {
        protected Wallet() { }

        public Wallet(string name)
        {
            Name = name;
            WalletAssets = new List<WalletAsset>();
        }

        public string Name { get; private set; }

        public virtual IList<WalletAsset> WalletAssets { get; set; }

    }
}
