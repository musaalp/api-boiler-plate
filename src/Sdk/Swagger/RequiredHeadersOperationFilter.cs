using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Sdk.Swagger
{
    /// <summary>
    ///     Required headers for http requests.
    /// </summary>
    public class RequiredHeadersOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Culture",
                In = ParameterLocation.Header,
                Required = true,
                Description = "Culture for localization ex. en-US",
                Schema = new OpenApiSchema { Default = new OpenApiString("en-US") }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Timezone",
                In = ParameterLocation.Header,
                Required = true,
                Description = "Timezone for dates ex. Europe/Istanbul",
                Schema = new OpenApiSchema { Default = new OpenApiString("Europe/Istanbul") }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-ClientId",
                In = ParameterLocation.Header,
                Required = true,
                Description = "Unique identifier of API consumer.",
                Schema = new OpenApiSchema { Default = new OpenApiInteger(1) }
            });
        }
    }
}
