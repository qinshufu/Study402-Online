using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Study402Online.Common.Configurations
{
    public class JsonContentTypeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters)
            {
                var paramDescription = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);

                parameter.Example = null;
                parameter.Content.Clear();
                parameter.Content["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(paramDescription.Type, context.SchemaRepository),
                    Example = null
                };

            }

            if (operation.RequestBody is not null)
            {

                operation.RequestBody.Content.Clear();
                operation.RequestBody.Content["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(context.MethodInfo.ReturnType, context.SchemaRepository),
                    Example = null
                };

            }

            foreach (var response in operation.Responses.Values)
            {
                response.Content.Clear();
                response.Content["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(context.MethodInfo.ReturnType, context.SchemaRepository),
                    Example = null
                };

            }
        }
    }
}