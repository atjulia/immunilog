using Immunilog.Domain.Entities;
using Immunilog.Repositories.DbContexts.Base;
using Microsoft.EntityFrameworkCore;

namespace Immunilog.Repositories.DbContexts;

public class ApiBaseDbContext : DbContextBase
{
    public ApiBaseDbContext(DbContextOptions<ApiBaseDbContext> options)
        : base(options)
    {
    }

    protected ApiBaseDbContext()
    {
    }

    public DbSet<Pessoa> Pessoa { get; set; } = default!;
    public DbSet<Usuario> Usuario { get; set; } = default!;
}
