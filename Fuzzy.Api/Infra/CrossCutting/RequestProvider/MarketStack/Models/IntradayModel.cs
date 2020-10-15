using System;
using System.Collections.Generic;

namespace Fuzzy.Api.Infra.CrossCutting.RequestProvider.MarketStack.Models
{
    public class IntradayModel
    {
        public PaginationModel Pagination { get; set; }
        public IEnumerable<IntradayAssetModel> Data { get; set; }
    }

    public class IntradayAssetModel
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Last { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
    }
}
