using BolaoCopa.API.Data;
using BolaoCopa.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolaoCopa.API.Services;

public class UsuarioService(BolaoDbContext db)
{
    public async Task<(bool sucesso, string erro, Usuario? usuario)> CadastrarAsync(
        string nome, string telefone, string chavePix)
    {
        var existe = await db.Usuarios.AnyAsync(u => u.Telefone == telefone);
        if (existe)
            return (false, "Já existe um usuário com esse telefone.", null);

        var usuario = new Usuario { Nome = nome, Telefone = telefone, ChavePix = chavePix };
        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();

        return (true, string.Empty, usuario);
    }
}
