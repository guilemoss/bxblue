using System;

namespace Fuzzy.Api.Domain.Models
{
    public class AssetForecastModel
    {
        public AssetForecastModel(Guid id, DateTime date, decimal price, int value, AssetModel asset)
        {
            Id = id;
            Date = date;
            Price = price;
            Value = value;
            Asset = asset;
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }
        public int Value { get; set; }

        public AssetModel Asset { get; set; }

    }
}
