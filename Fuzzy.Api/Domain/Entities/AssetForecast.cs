using System;

namespace Fuzzy.Api.Domain.Entities
{
    public class AssetForecast : Entity<Guid>
    {
        protected AssetForecast() { }

        public AssetForecast(Guid assetId, decimal price)
        {
            Date = DateTime.Now;
            Price = price;
            AssetId = assetId;
        }

        public DateTime Date { get; private set; }

        public decimal Price { get; set; }

        public Guid AssetId { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
