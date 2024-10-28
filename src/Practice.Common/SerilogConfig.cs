using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Practice.Common
{
    public class SerilogConfig
    {
        public static void Prepare(string applicationName, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string podName = configuration.GetValue("PODNAME", defaultValue: "");

            LoggerConfiguration loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("AppName", applicationName)
                .Enrich.WithProperty("PodName", podName)
                .WriteTo.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                    levelSwitch: new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Warning)
                    );

            Log.Logger = loggerConfig.CreateLogger();
        }
    }
}
