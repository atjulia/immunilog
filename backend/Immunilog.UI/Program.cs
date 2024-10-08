using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json.Converters;
using PG.Immunilog.Configurations;
using PG.Immunilog.UI.Configurations;
using Serilog;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Immunilog.Services.Services.Autenticacao;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var appConfigFile = Environment.GetEnvironmentVariable("APP_CONFIG_FILE");
if (!string.IsNullOrEmpty(appConfigFile) && File.Exists(appConfigFile))
{
    builder.Configuration.AddJsonFile(appConfigFile);
}

builder.Host.UseSerilog((hostContext, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(hostContext.Configuration)
        .Enrich.WithProperty("ApplicationName", hostContext.HostingEnvironment.ApplicationName);
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddApiBaseServices(builder.Configuration);
builder.Services.ResolveDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

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

app.UseSerilogRequestLogging();

app.Services.MigrateDatabaseAsync().Wait();

var supportedCultures = new[] { new CultureInfo("pt-BR") };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseHttpsRedirection();
app.UseAuthentication(); // Adiciona middleware de autenticação
app.UseAuthorization();  // Adiciona middleware de autorização

app.MapControllers();

await app.RunAsync();
