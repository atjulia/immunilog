﻿using Immunilog.Domain.Dto.Usuario;
using Immunilog.Domain.Entities;
using Immunilog.Repositories.DbContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Immunilog.Repositories.Repositories;

public interface IUsuarioRepository
{
    Task<List<UsuarioDto>> GetListAsync();
    Task<UsuarioDto?> GetUsuarioById(Guid id);
    Task<Guid> CreateAsync(CreationUsuarioDto data);
    Task UpdateAsync(UsuarioDto data);
    Task<bool> DeleteAsync(Guid id);
    Task<UsuarioDto> GetUserByEmail(string email);
}
public class UsuarioRepository : IUsuarioRepository
{
    public ApiBaseDbContext dbContext { get; }

    public UsuarioRepository(ApiBaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<UsuarioDto>> GetListAsync()
    {
        return await dbContext.Usuario
            .AsNoTracking()
            .ProjectToType<UsuarioDto>()
            .ToListAsync();
    }

    public async Task<UsuarioDto?> GetUsuarioById(Guid id)
    {
        return await dbContext.Usuario
            .AsNoTracking()
            .ProjectToType<UsuarioDto>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<UsuarioDto?> GetUserByEmail(string email)
    {
        return await dbContext.Usuario
            .AsNoTracking()
            .ProjectToType<UsuarioDto>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Guid> CreateAsync(CreationUsuarioDto data)
    {
        var newUsuario = new Usuario
        {
            Id = Guid.NewGuid(),
            DtCriacao = DateTime.Now,
            Nome = data.Nome,
            Email = data.Email,
            Senha = data.Senha,
            IsActive = data.IsActive,
            Role = data.Role
        };

        await dbContext.Usuario.AddAsync(newUsuario);
        await dbContext.SaveChangesAsync();

        return newUsuario.Id;
    }

    public async Task UpdateAsync(UsuarioDto data)
    => await dbContext.Usuario
        .AsNoTracking()
        .Where(c => c.Id == data.Id)
        .UpdateAsync(c => new Usuario
        {
            Nome = data.Nome.ToUpper(),
            Email = data.Email,
            IsActive = data.IsActive,
            Role = data.Role,
            DtUpdate = DateTime.Now,
        });


    public async Task<bool> DeleteAsync(Guid id)
    {
        var usuario = await dbContext.Usuario
            .FirstOrDefaultAsync(pu => pu.Id == id);

        if (usuario == null)
            return false;

        dbContext.Usuario.Remove(usuario);
        await dbContext.SaveChangesAsync();

        return true;
    }
}
