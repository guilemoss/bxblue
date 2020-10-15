namespace Fuzzy.Api.Infra.CrossCutting.RequestProvider.MarketStack.Models
{
    public class PaginationModel
    {
        public int Limit { get; set; }
        public int OffSet { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}
