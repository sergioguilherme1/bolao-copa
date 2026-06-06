using BolaoCopa.API.Models;
using BolaoCopa.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BolaoCopa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApostasController(ApostaService service) : ControllerBase
{
    public record CriarApostaRequest(
        int UsuarioId,
        int JogoId,
        int GolsTimeA,
        int GolsTimeB,
        decimal ValorApostado,
        FormaPagamento FormaPagamento);

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarApostaRequest req)
    {
        var resultado = await service.CriarAsync(new Services.CriarApostaRequest(
            req.UsuarioId, req.JogoId, req.GolsTimeA, req.GolsTimeB, req.ValorApostado, req.FormaPagamento));

        if (!resultado.Sucesso)
            return BadRequest(new { resultado.Erro });

        return CreatedAtAction(nameof(Criar), new { id = resultado.ApostaId }, new
        {
            resultado.ApostaId,
            resultado.StatusPagamento,
            resultado.PixCopiaCola
        });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var (sucesso, erro) = await service.CancelarAsync(id);
        if (!sucesso)
            return BadRequest(new { erro });

        return NoContent();
    }
}
