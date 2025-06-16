using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevTest_API.Configuration
{
    public class RemoveResponseExampleFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses.ContainsKey("401"))
            {
                var response = operation.Responses["401"];
                response.Content?.Clear();
            }
        }
    }
}
