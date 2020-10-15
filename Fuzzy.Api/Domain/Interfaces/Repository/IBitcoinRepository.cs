using Fuzzy.Api.Domain.Models;
using System.Threading.Tasks;

namespace Fuzzy.Api.Domain.Interfaces.Repository
{
    public interface IBitcoinRepository
    {
        Task<AssetForecastModel> GetBitcoinAsync(int valueToApply);
    }
}
