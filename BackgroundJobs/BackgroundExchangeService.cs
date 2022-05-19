using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJobs
{
    public class BackgroundExchangeService : BackgroundService
    {
        private readonly ILogger<BackgroundExchangeService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly BackgroundJobModel _backgroundModel;

        public BackgroundExchangeService(ILogger<BackgroundExchangeService> logger, IServiceProvider serviceProvider, BackgroundJobModel backgroundModel)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _backgroundModel = backgroundModel;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_backgroundModel.IsActive)
            {
                _logger.LogInformation("Exchange Background Service is working");

                while (!cancellationToken.IsCancellationRequested)
                {
                    await GetValues();
                    await Task.Delay(TimeSpan.FromMinutes(_backgroundModel.IntervalMinutes), cancellationToken);
                }
            }
        }

        private async Task GetValues()
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var exchangeService = scope.ServiceProvider.GetRequiredService<IExchangeService>();

                await exchangeService.UpdateExchage();
                var values = await exchangeService.GetValues();

                foreach (var value in values)
                {
                    _logger.LogInformation("Value: {0}", value.ExchangeCurrency);
                }
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Exchange Background Service is stoping");
            return base.StopAsync(cancellationToken);
        }
    }
}
