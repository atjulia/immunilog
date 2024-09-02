using Immunilog.Repositories;
using Immunilog.Services.Services.Pessoa;
using Immunilog.Services.Services.Usuario;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddApiBaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiBaseRepositories(configuration);

        //services.AddScoped<INotifier, Notifier>();

        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }

    public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
        await migrator.MigrateAsync();
    }
}
