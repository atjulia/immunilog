﻿using Immunilog.Repositories.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Immunilog.Repositories.Repositories;

public class DatabaseMigrator : IDatabaseMigrator
{
    private readonly ApiBaseDbContext dbContext;

    public DatabaseMigrator(ApiBaseDbContext dbContext)
        => this.dbContext = dbContext;

    public async Task MigrateAsync()
    {
        await dbContext.Database.MigrateAsync();
        await dbContext.SaveChangesAsync();
    }
}
