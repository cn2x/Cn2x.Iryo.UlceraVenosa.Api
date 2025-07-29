using Cn2x.Iryo.UlceraVenosa.Api.Extensions;
using Cn2x.Iryo.UlceraVenosa.Api.Middleware;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Extensions;
using Cn2x.Iryo.UlceraVenosa.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configurações de performance do Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    var kestrelSection = builder.Configuration.GetSection("Kestrel");
    
    options.Limits.MaxConcurrentConnections = kestrelSection.GetValue<int>("Limits:MaxConcurrentConnections", 100);
    options.Limits.MaxConcurrentUpgradedConnections = kestrelSection.GetValue<int>("Limits:MaxConcurrentUpgradedConnections", 100);
    options.Limits.MaxRequestBodySize = kestrelSection.GetValue<long>("Limits:MaxRequestBodySize", 52428800);
    options.Limits.KeepAliveTimeout = TimeSpan.Parse(kestrelSection.GetValue<string>("Limits:KeepAliveTimeout", "00:02:00"));
    options.Limits.RequestHeadersTimeout = TimeSpan.Parse(kestrelSection.GetValue<string>("Limits:RequestHeadersTimeout", "00:00:30"));
});

// Adiciona serviços da aplicação
builder.Services.AddApplicationServices(builder.Configuration);

// Configuração do banco de dados
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddApplicationDbContextForDevelopment(builder.Configuration);
    builder.Services.AddGraphQLServicesForDevelopment();
}
else
{
    builder.Services.AddApplicationDbContext(builder.Configuration);
    builder.Services.AddGraphQLServices();
}

// Configuração dos Health Checks
builder.Services.AddHealthChecksServices(builder.Configuration);

// Configurações de controllers
// builder.Services.AddControllers();

var app = builder.Build();

// Middleware de tratamento de erros
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configurações de performance
app.UseResponseCompression();

// Configurações de CORS
app.UseCors("AllowedOrigins");

// Configurações de roteamento
app.UseRouting();

// Configurações de GraphQL
app.MapGraphQL();

// Configuração dos Health Checks
app.UseHealthChecks();

// Root endpoint
app.MapGet("/", () => Results.Ok(new { 
    message = "Úlcera Venosa API is running", 
    graphql = "/graphql",
    health = "/health",
    healthReady = "/health/ready",
    healthLive = "/health/live",
    version = "1.0.0"
}));

app.Run();
public partial class Program { }
