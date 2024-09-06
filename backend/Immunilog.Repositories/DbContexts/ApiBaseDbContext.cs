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
    public DbSet<Vacina> Vacina { get; set; } = default!;
    public DbSet<Doenca> Doenca { get; set; }
    public DbSet<VacinaDoenca> VacinaDoencas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VacinaDoenca>()
            .HasKey(vd => new { vd.VacinaId, vd.DoencaId });

        modelBuilder.Entity<VacinaDoenca>()
            .HasOne(vd => vd.Vacina)
            .WithMany(v => v.VacinaDoencas)
            .HasForeignKey(vd => vd.VacinaId);

        modelBuilder.Entity<VacinaDoenca>()
            .HasOne(vd => vd.Doenca)
            .WithMany(d => d.VacinaDoencas)
            .HasForeignKey(vd => vd.DoencaId);
    }
}