using Immunilog.Repositories;
using Immunilog.Services.Services.Pessoa;
using Immunilog.Services.Services.Usuario;
using Immunilog.Services.Services.Vacina;
using Immunilog.Services.Services.VacinaPessoa;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddApiBaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiBaseRepositories(configuration);

        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IVacinaService, VacinaService>(); 
        services.AddScoped<IVacinaPessoaService, VacinaPessoaService>();

        return services;
    }

    public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
        await migrator.MigrateAsync();
    }
}
