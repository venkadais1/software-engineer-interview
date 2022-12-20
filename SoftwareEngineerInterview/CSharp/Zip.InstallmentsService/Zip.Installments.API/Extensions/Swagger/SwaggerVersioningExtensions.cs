using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Zip.Installments.API.Extensions.Swagger.Options;

namespace Zip.Installments.API.Extensions.Swagger
{
    public static class SwaggerVersioningExtensions
    {
        /// <summary>
        ///     Add Api versioning to choose versions
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(option => 
            {
                option.DefaultApiVersion = new ApiVersion(1,2);
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
        ///     Add Explorer to discover vesions
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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
