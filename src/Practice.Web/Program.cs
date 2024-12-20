using Serilog;
using Practice.Common;

namespace Practice.Web
{
    public class Program
    {
        public static readonly string AppName = typeof(Program).Namespace;

        public static readonly Guid Id = Guid.NewGuid();

        public static int Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            SerilogConfig.Prepare(AppName, configuration);

            try
            {
                Log.Information("[{AppName}] [{Id}] Starting web host", AppName, Id);

                SetThreadPool();

                CreateHostBuilder(args).Build().Run();

                Console.WriteLine("Press any key to exit");

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "[{AppName}] [{Id}] Host terminated unexpectedly: {errorMessage}",
                    AppName, Id, ex.Message);
                return 1;
            }
            finally
            {
                Log.Information("[{AppName}] [{Id}] Host terminated", AppName, Id);
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(kesOpt =>
                    {
                        kesOpt.AddServerHeader = false;
                        kesOpt.Limits.MaxRequestBodySize = 130 * 1024 * 1024;
                    });
                    webBuilder.UseStartup<Startup>();
                });
        }

        private static void SetThreadPool()
        {
            ThreadPool.GetMaxThreads(out int maxWorkers, out int maxIOs);

            int newMaxWorkers = maxWorkers > 1000
                ? 1000
                : maxWorkers;
            int newMaxIOs = maxIOs > 500
                ? 500
                : maxIOs;

            if (ThreadPool.SetMaxThreads(newMaxWorkers, newMaxIOs))
            {
                Log.Information($"Set max worker thread count ({maxWorkers}) to {newMaxWorkers}");
                Log.Information($"Set max IO thread count ({maxIOs}) to {newMaxIOs}");
            }
            else
            {
                Log.Error($"SetMaxThreads fail");
            }

            ThreadPool.GetMinThreads(out int minWorkers, out int minIOs);

            int newMinWorkers = minWorkers * 32;
            if (newMinWorkers > newMaxWorkers)
            {
                newMinWorkers = newMaxWorkers;
            }

            int newMinIOs = minIOs * 16;
            if (newMinIOs > newMaxIOs)
            {
                newMinIOs = newMaxIOs;
            }

            if (ThreadPool.SetMinThreads(newMinWorkers, newMinIOs))
            {
                Log.Information($"Set min worker thread count ({minWorkers}) to {newMinWorkers}");
                Log.Information($"Set min IO thread count ({minIOs}) to {newMinIOs}");
            }
            else
            {
                Log.Error($"SetMinThreads fail");
            }
        }
    }
}