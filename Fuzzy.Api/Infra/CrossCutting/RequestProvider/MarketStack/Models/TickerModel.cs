using System.Collections.Generic;

namespace Fuzzy.Api.Infra.CrossCutting.RequestProvider.MarketStack.Models
{
    public class TickerModel
    {
        public PaginationModel Pagination { get; set; }
        public IEnumerable<TickerAssetModel> Data { get; set; }
    }

    public class TickerAssetModel {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
