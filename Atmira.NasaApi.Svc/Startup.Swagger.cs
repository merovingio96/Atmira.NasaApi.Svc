using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Atmira.NasaApi.Svc
{
    public partial class Startup
    {
        private class SwaggerDefaultValues : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                {
                    return;
                }

                List<OpenApiParameter> parametersToRemove = operation.Parameters.Where(parameter => parameter.Name == "api-version").ToList();

                foreach (OpenApiParameter parameter in parametersToRemove)
                {
                    operation.Parameters.Remove(parameter);
                }

                foreach (OpenApiParameter parameter in operation.Parameters)
                {
                    ApiParameterDescription description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);
                    ApiParameterRouteInfo routeInfo = description?.RouteInfo;

                    if (parameter.Description == null)
                    {
                        parameter.Description = description?.ModelMetadata?.Description;
                    }

                    if (routeInfo == null || routeInfo.DefaultValue == null)
                    {
                        continue;
                    }

                    if (parameter.Schema.Default == null)
                    {
                        parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());
                    }

                    parameter.Required |= !routeInfo.IsOptional;
                }
            }
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
            });
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            app.UseSwaggerUI();
        }
    }
}
