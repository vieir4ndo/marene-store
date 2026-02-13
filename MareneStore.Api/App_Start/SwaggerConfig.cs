using System.Web.Http;
using WebActivatorEx;
using MareneStore.Api;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MareneStore.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "MareneStore.Api");

                        c.ApiKey("Bearer")
                            .Description("Ingrese el token JWT en el campo. Ejemplo: Bearer {token}")
                            .Name("Authorization")
                            .In("header");

                        c.OperationFilter<AssignBearerTokenSecurityRequirements>();
                    })
                .EnableSwaggerUi(c =>
                    {
                        // Habilitar soporte para el esquema de seguridad
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }

    public class AssignBearerTokenSecurityRequirements : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                return;

            operation.security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                }
            };
        }
    }
}
