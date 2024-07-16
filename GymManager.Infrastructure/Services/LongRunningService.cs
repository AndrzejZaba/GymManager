using GymManager.Application.Common.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GymManager.Infrastructure.Services;

public class LongRunningService : BackgroundService
{
    private readonly IBackgroundWorkerQueue _queue;
    private readonly ILogger<LongRunningService> _logger;

    public LongRunningService(
        IBackgroundWorkerQueue queue,
        ILogger<LongRunningService> logger)
    {
        _queue = queue;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var workItem = await _queue.DequeueAsync(stoppingToken);

                _logger.LogInformation("ExecuteAsync Start...");

                await workItem(stoppingToken);

                _logger.LogInformation("ExecuteAsync Stop...");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, null);
            }
        }
    }
}
