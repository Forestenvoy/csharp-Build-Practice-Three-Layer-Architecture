using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using Serilog;
using Practice.Common;
using Practice.Common.ViewModels;
using Practice.Web.Configuration;
using Practice.Web.Middlewares;
using Practice.Repository.Persistence;
using Practice.Application.Services;

namespace Practice.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    option.SerializerSettings.Converters.Add(new UnixTimestampDateTimeConvert());
                })
                .ConfigureApiBehaviorOptions(option =>
                {
                    // ModelState 驗證失敗時, 回傳自訂的錯誤訊息
                    option.SuppressModelStateInvalidFilter = false;
                    option.SuppressMapClientErrors = true;
                    option.InvalidModelStateResponseFactory = actionContext =>
                    {
                        string msg = null;

                        if (actionContext.ModelState.ErrorCount > 0)
                        {
                            msg = actionContext.ModelState
                                .FirstOrDefault(o => o.Value.ValidationState == ModelValidationState.Invalid).Key ?? string.Empty;
                        }

                        // 200 ok
                        return new JsonResult(new { code = ResponseCode.BAD_PARAMS, msg });
                    };
                });

            services.AddCustomSwaggerGen();

            services.AddMySqlDatabase(Configuration);

            ConfigureService(services);
        }

        private void ConfigureService(IServiceCollection services)
        {
            services.AddDataProtection()
                .SetApplicationName("Practice.Web")
                //.PersistKeysToFileSystem(new DirectoryInfo(@"./Auth/dataprotection-persistkeys"))
                .PersistKeysToDbContext<PracticeDbContext>()
                .AddKeyManagementOptions(options =>
                {
                    options.NewKeyLifetime = new TimeSpan(365 * 3, 0, 0, 0);
                    options.AutoGenerateKeys = true;
                });

            // Web
            services.AddScoped<AdminService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/{SwaggerConst.BackendDocs}/swagger.json",
                        "Practice.Web v1 for Backend");

                    options.SwaggerEndpoint($"/swagger/{SwaggerConst.FrontendDocs}/swagger.json",
                        "Practice.Web v1 for Frontend");
                });
            }

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseMiddleware<GlobalErrorHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
