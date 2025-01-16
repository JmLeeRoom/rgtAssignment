using Serilog;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class LogService
    {
        public LogService()
        {
        
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("D:\\Orders\\logs\\orderservice.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogError(string message, Exception ex)
        {
            Log.Error(ex, message);
        }

        public void Dispose()
        {
            Log.CloseAndFlush();
        }
    }
}
