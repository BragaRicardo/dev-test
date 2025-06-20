using DevTest_API.Configuration;
using DevTest_API.Data;
using DevTest_API.Repositories;
using DevTest_API.Repositories.Interfaces;
using DevTest_API.Services;
using DevTest_API.Services.Interfaces;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = "DevTest-API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DevTest-API",
        Version = "v1",
        Description = "Esta API permite gestionar usuarios, autenticación y autorización mediante JWT. Incluye operaciones CRUD para usuarios y autenticación segura.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Ricardo Braga",
            Email = "bragaricardo2022@gmail.com"
        }
    });

    c.DocumentFilter<ApplyTagDescriptions>();
    c.OperationFilter<RemoveResponseExampleFilter>();
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Ingrese 'Bearer' seguido de un espacio y el token JWT",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddHealthChecks();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? new JwtSettings();

jwtSettings.Key = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key no está configurado en User Secrets");

var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

var baseConnString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection no está configurada");
var password = builder.Configuration["DB_PASSWORD"]
    ?? throw new InvalidOperationException("DB_PASSWORD no está configurada en User Secrets");
var connBuilder = new Npgsql.NpgsqlConnectionStringBuilder(baseConnString)
{
    Password = password
};
var finalConnString = connBuilder.ConnectionString;

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(finalConnString, npgsqlOpts =>
        npgsqlOpts.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SoloAdmin", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("AdminOConsultor", policy => policy.RequireRole("ADMIN", "CONSULTOR"));
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:3000",
                "https://dev-test-hmn18sq5i-ricardo-bragas-projects-add6e9d9.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
builder.Services.AddProblemDetails(opts =>
{
    // Mapear Unauthorized como 401
    opts.Map<UnauthorizedAccessException>(ex =>
        new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Title = "No autorizado",
            Status = StatusCodes.Status401Unauthorized,
            Detail = ex.Message
        });
    // Mapear ArgumentException como 400
    opts.Map<ArgumentException>(ex =>
        new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Title = "Solicitud inválida",
            Status = StatusCodes.Status400BadRequest,
            Detail = ex.Message
        });
    // Todas las excepciones no mapeadas a 500
    opts.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});
var app = builder.Build();

app.UseProblemDetails();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var strategy = db.Database.CreateExecutionStrategy();
    strategy.Execute(() => db.Database.Migrate());
    Console.WriteLine("✔ Migraciones aplicadas correctamente");
}
catch (Exception ex)
{
    Console.WriteLine("⚠ Error al aplicar migraciones:");
    Console.WriteLine(ex.Message);
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();