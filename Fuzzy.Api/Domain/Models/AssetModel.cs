
using System;

namespace Fuzzy.Api.Domain.Models
{
    public class AssetModel
    {
        public AssetModel() {}

        public AssetModel(Guid id, string symbol, string name, string type)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
            Type = type;
        }

        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

    }
}
