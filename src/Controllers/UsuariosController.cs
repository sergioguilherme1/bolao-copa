using BolaoCopa.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BolaoCopa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(UsuarioService service) : ControllerBase
{
    public record CriarUsuarioRequest(string Nome, string Telefone, string ChavePix);

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioRequest req)
    {
        var (sucesso, erro, usuario) = await service.CadastrarAsync(req.Nome, req.Telefone, req.ChavePix);
        if (!sucesso)
            return Conflict(new { erro });

        return CreatedAtAction(nameof(Criar), new { id = usuario!.Id }, new { usuario.Id });
    }
}
