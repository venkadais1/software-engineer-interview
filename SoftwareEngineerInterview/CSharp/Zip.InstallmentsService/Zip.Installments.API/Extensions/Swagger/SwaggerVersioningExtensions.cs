using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Zip.Installments.API.Extensions.Swagger.Options;

namespace Zip.Installments.API.Extensions.Swagger
{
    public static class SwaggerVersioningExtensions
    {
        /// <summary>
        ///     Add API version to choose versions
        /// </summary>
        /// <param name="services">Add this extension on service collections</param>
        /// <returns>Returns to service collection</returns>
        public static IServiceCollection AddSwaggApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new ApiVersion(1, 2);
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("api-version"),
                    new MediaTypeApiVersionReader("api-version"));
            });
            return services;
        }

        /// <summary>
        ///     Add Explorer to discover version
        /// </summary>
        /// <param name="services">Add this extension on service collections</param>
        /// <returns>Returns to service collection</returns>
        public static IServiceCollection AddSwagerApiVersionExplorer(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            return services;
        }

        /// <summary>
        ///     Swagger setup for UI rendering with version
        /// </summary>
        /// <param name="app">Add this extension on application builders</param>
        /// <returns>Returns an application builder</returns>
        public static IApplicationBuilder UseSwaggerUISetup(this IApplicationBuilder app)
        {
            var apiVersionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpper());
                }
            });
            return app;
        }
    }
}
