using BolaoCopa.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BolaoCopa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhooksController(ApostaService apostaService) : ControllerBase
{
    public record PagamentoWebhookRequest(int ApostaId, string Status);

    [HttpPost("pagamento")]
    public async Task<IActionResult> Pagamento([FromBody] PagamentoWebhookRequest req)
    {
        if (!string.Equals(req.Status, "approved", StringComparison.OrdinalIgnoreCase))
            return Ok(new { mensagem = "Status ignorado." });

        var (sucesso, erro) = await apostaService.ConfirmarPagamentoAsync(req.ApostaId);
        if (!sucesso)
            return BadRequest(new { erro });

        return Ok(new { mensagem = "Pagamento confirmado." });
    }
}
