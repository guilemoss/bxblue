using Fuzzy.Api.Domain.Models;
using System.Threading.Tasks;

namespace Fuzzy.Api.Domain.Interfaces.Service
{
    public interface IBitcoinService
    {
        Task<AssetForecastModel> GetBitcoinAsync(int valueToApply);
    }
}
