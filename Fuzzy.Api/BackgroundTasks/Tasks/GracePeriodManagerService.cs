using Fuzzy.Api.Domain.Interfaces.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Fuzzy.Api.BackgroundTasks.Tasks
{
    public class GracePeriodManagerService : BackgroundService
    {
        private readonly ILogger<GracePeriodManagerService> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IAssetService _assetService;
        
        public GracePeriodManagerService(IOptions<BackgroundTaskSettings> settings, IAssetService assetService, ILogger<GracePeriodManagerService> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _assetService = assetService ?? throw new ArgumentNullException(nameof(assetService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");
            stoppingToken.Register(() => _logger.LogDebug("#1 GracePeriodManagerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("GracePeriodManagerService background task is doing background work.");
                await _assetService.PopulateAssetsForecast(100, 50);
                await Task.Delay((_settings.CheckUpdateTime * 1000), stoppingToken);
            }

            _logger.LogDebug("GracePeriodManagerService background task is stopping.");
            await Task.CompletedTask;
        }
    }
}
