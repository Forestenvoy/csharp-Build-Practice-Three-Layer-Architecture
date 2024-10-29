using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

namespace Practice.Web.Configuration
{
    public static class ConfigureSwagger
    {
        private const string Version = "v1";

        private static readonly string DocsDescription =
@$"

## 時間格式

時間欄位格式為 unix timestamp (milliseconds), 目前 swagger 因 bug 顯示為 字串 yyyy-MM-dd HH:mm:ss

## 返回 code

請參考 `ResponseCode` 列舉

## 參數正則表達式

如果該參數有正則表達式規則, 請參照 `swagger.json`, 目前 swagger ui 有 bug 無法正常顯示...

";

        public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // 基本資訊
                options.SwaggerDoc(SwaggerConst.BackendDocs, new OpenApiInfo
                {
                    Version = Version,
                    Title = Assembly.GetExecutingAssembly().GetName().Name + " v1 for Backend",
                    Description = DocsDescription,
                });
                options.SwaggerDoc(SwaggerConst.FrontendDocs, new OpenApiInfo
                {
                    Version = Version,
                    Title = Assembly.GetExecutingAssembly().GetName().Name + " v1 for frontend",
                    Description = DocsDescription
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // 列舉值顯示 XML 註釋
                options.AddEnumsWithValuesFixFilters(o =>
                {
                    o.ApplySchemaFilter = true;
                    o.ApplyParameterFilter = true;
                    o.ApplyDocumentFilter = true;
                    o.IncludeDescriptions = true;
                    o.IncludeXEnumRemarks = true;
                    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
                    o.IncludeXmlCommentsFrom(xmlPath);
                });

                // 自訂 SchemaId ==> 完整顯示命名空間加上類別名稱
                options.CustomSchemaIds(type => type.ToString());

            }).AddSwaggerGenNewtonsoftSupport();


            return services;
        }
    }

    public static class SwaggerConst
    {
        public const string BackendDocs = "back";
        public const string FrontendDocs = "front";
    }
}
