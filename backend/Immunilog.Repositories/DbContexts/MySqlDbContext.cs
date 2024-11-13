using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Immunilog.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Immunilog.Repositories.DbContexts;

public class MySqlDbContext : ApiBaseDbContext
{
    private readonly IConfiguration configuration;

    public MySqlDbContext(IConfiguration configuration)
        => this.configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = configuration["ConnectionString"];

        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay = TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
        };

        var client = new SecretClient(new Uri(configuration["KeyVaultUrl"]), new DefaultAzureCredential(), options);

        var dbUser = client.GetSecret("db-user").Value.Value;
        var dbPassword = client.GetSecret("db-password").Value.Value;

        connectionString = connectionString.Replace("{db-user}", dbUser)
                                           .Replace("{db-password}", dbPassword);

        var versionText = configuration["MySqlVersion"];
        var version = ServerVersion.Parse(versionText);

        optionsBuilder.UseMySql(connectionString, version);
    }
}
