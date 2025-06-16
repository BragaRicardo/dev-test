using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevTest_API.Configuration
{
    public class ApplyTagDescriptions : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
        {
            new OpenApiTag { Name = "Auth", Description = "Operaciones de autenticación y generación de tokens JWT" },
            new OpenApiTag { Name = "Usuarios", Description = "CRUD de usuarios del sistema" }
        };
        }
    }
}
