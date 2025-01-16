using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly OrderProcessor _orderProcessor;
        private readonly string _inputPath = "D:\\Orders\\Input";
        private readonly string _outputPath = "D:\\Orders\\Processed";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _orderProcessor = new OrderProcessor();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderService Worker running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // 주문 처리 실행
                    _orderProcessor.ProcessOrders(_inputPath, _outputPath);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in order processing: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // 30초 간격으로 실행
            }
        }
    }
}
