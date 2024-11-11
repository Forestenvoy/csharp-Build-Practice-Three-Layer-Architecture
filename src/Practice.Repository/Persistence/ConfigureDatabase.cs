using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Practice.Repository.Persistence
{
    public static class ConfigureDatabase
    {
        public static IServiceCollection AddMySqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            MySqlConnectionStringBuilder dbConnStringBuilder = new(configuration.GetValue<string>("DB"));

            Log.Information($"Database [{nameof(PracticeDbContext)}] Server: '{dbConnStringBuilder.Server}'");

            dbConnStringBuilder.TreatTinyAsBoolean = false;

            ServerVersion serverVersion = null;

            services.AddDbContext<PracticeDbContext>(options =>
            {
                if (serverVersion == null)
                {
                    serverVersion = ServerVersion.AutoDetect(dbConnStringBuilder.ConnectionString);
                }

                options.UseMySql(dbConnStringBuilder.ConnectionString, serverVersion,
                    op =>
                    {
                        op.DefaultDataTypeMappings(m => m.WithClrBoolean(MySqlBooleanType.Bit1));
                        op.DefaultDataTypeMappings(m => m.WithClrDateTime(MySqlDateTimeType.Timestamp6));
                    });
            });

            InitializeDatabase(configuration, services);

            return services;
        }

        private static void InitializeDatabase(IConfiguration configuration, IServiceCollection services)
        {
            if (configuration.GetValue("InitializeDatabase", false))
            {
                Log.Information($"InitializeDatabase is 'true', 若DB未建立則自動建立DB與TABLE");

                // 確保資料庫已建立
                services.BuildServiceProvider().GetRequiredService<PracticeDbContext>()
                        .Database.EnsureCreated();
            }
        }
    }
}
