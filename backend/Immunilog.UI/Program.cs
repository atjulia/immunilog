using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Immunilog.Services.Services.Autenticacao;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using PG.Immunilog.Configurations;
using PG.Immunilog.UI.Configurations;
using Serilog;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

var appConfigFile = Environment.GetEnvironmentVariable("APP_CONFIG_FILE");
if (!string.IsNullOrEmpty(appConfigFile) && File.Exists(appConfigFile))
{
    builder.Configuration.AddJsonFile(appConfigFile);
}

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddApiBaseServices(builder.Configuration);
builder.Services.ResolveDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();

var keyVaultUrl = builder.Configuration["KeyVaultUrl"];
if (string.IsNullOrEmpty(keyVaultUrl))
{
    throw new ArgumentNullException("KeyVaultUrl", "A configuração 'KeyVaultUrl' não pode ser nula ou vazia.");
}

var credential = new DefaultAzureCredential();
var client = new SecretClient(new Uri(keyVaultUrl), credential);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

try
{
    app.Services.MigrateDatabaseAsync().Wait();
}
catch (Exception ex)
{
    Console.WriteLine($"Erro durante a migração do banco de dados: {ex.Message}");
    throw;
}

var supportedCultures = new[] { new CultureInfo("pt-BR") };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
