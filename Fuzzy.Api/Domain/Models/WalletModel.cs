using System;

namespace Fuzzy.Api.Domain.Models
{
    public class WalletModel
    {
        public WalletModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
