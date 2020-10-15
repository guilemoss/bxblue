using Fuzzy.Api.Domain.Interfaces.Repository;
using Fuzzy.Api.Domain.Interfaces.Service;
using Fuzzy.Api.Domain.Models;
using System.Threading.Tasks;

namespace Fuzzy.Api.Services
{
    public class BitcoinService : IBitcoinService
    {
        private readonly IBitcoinRepository _bitcoinRepository;
        public BitcoinService(IBitcoinRepository bitcoinRepository)
        {
            _bitcoinRepository = bitcoinRepository;
        }

        public async Task<AssetForecastModel> GetBitcoinAsync(int valueToApply)
        {
            return await _bitcoinRepository.GetBitcoinAsync(valueToApply);
        }
    }
}