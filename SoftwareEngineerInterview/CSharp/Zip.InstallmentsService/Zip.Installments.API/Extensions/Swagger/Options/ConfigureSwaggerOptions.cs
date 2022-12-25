using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zip.Installments.API.Extensions.Swagger.Options
{
    /// <summary>
    ///     The Definition of <see cref="ConfigureSwaggerOptions"/>
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        ///     Initialize an instance of <see cref="ConfigureSwaggerOptions"/>
        /// </summary>
        /// <param name="provider">An instance of version description provider</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in this.provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,
                    this.CreateVersionInfo(description));
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "ZipPay Payment API",
                Description = "To create installments of ZipPay paymnets",
                Version = description.ApiVersion.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += "The api Version has been depricated";
            }

            return info;
        }
    }
}
