namespace Immunilog.Repositories;

public interface IDatabaseMigrator
{
    Task MigrateAsync();
}