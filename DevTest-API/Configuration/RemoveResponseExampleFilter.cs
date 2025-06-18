using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevTest_API.Configuration
{
    public class RemoveResponseExampleFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var codigos = new[] { "400", "401", "403" };

            foreach (var codigo in codigos)
            {
                if (operation.Responses.ContainsKey(codigo))
                {
                    var response = operation.Responses[codigo];
                    response.Content?.Clear();
                }
            }
        }
    }
}
