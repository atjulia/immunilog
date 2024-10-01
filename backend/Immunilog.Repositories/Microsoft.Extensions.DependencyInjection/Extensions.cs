using Immunilog.Repositories;
using Immunilog.Repositories.DbContexts;
using Immunilog.Repositories.Repositories;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static void AddApiBaseRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["Provider"];
        switch (provider)
        {
            case "SqlServer":
                services.AddDbContext<ApiBaseDbContext, SqlServerDbContext>();
                break;
            case "MySql":
                services.AddDbContext<ApiBaseDbContext, MySqlDbContext>();
                break;
            default:
                throw new InvalidOperationException($"Provedor de banco de dados '{provider}' não suportado.");
        }

        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        services.AddDbContext<SqlServerDbContext>();
        services.AddDbContext<MySqlDbContext>();

        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IVacinaRepository, VacinaRepository>();
        services.AddScoped<IVacinaPessoaRepository, VacinaPessoaRepository>();

    }
}
