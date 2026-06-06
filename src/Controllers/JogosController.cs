using BolaoCopa.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BolaoCopa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogosController(ApuracaoService apuracaoService) : ControllerBase
{
    public record FinalizarJogoRequest(int GolsTimeA, int GolsTimeB);

    [HttpPost("{id}/finalizar")]
    public async Task<IActionResult> Finalizar(int id, [FromBody] FinalizarJogoRequest req)
    {
        var resultado = await apuracaoService.FinalizarJogoAsync(id, req.GolsTimeA, req.GolsTimeB);
        if (!resultado.Sucesso)
            return BadRequest(new { resultado.Erro });

        if (resultado.Acumulou)
        {
            return Ok(new
            {
                mensagem = "Nenhum acertador. O prêmio acumulou!",
                resultado.PoteTotal,
                resultado.SaldoAcumuladoAtualizado
            });
        }

        return Ok(new
        {
            mensagem = $"{resultado.Ganhadores.Count} ganhador(es) encontrado(s).",
            resultado.PoteTotal,
            ganhadores = resultado.Ganhadores
        });
    }
}
