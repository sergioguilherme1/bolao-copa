using BolaoCopa.API.Data;
using BolaoCopa.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolaoCopa.API.Services;

public record CriarApostaRequest(
    int UsuarioId,
    int JogoId,
    int GolsTimeA,
    int GolsTimeB,
    decimal ValorApostado,
    FormaPagamento FormaPagamento);

public record CriarApostaResult(
    bool Sucesso,
    string Erro,
    int? ApostaId,
    StatusPagamento? StatusPagamento,
    string? PixCopiaCola);

public class ApostaService(BolaoDbContext db)
{
    public async Task<CriarApostaResult> CriarAsync(CriarApostaRequest req)
    {
        var usuarioExiste = await db.Usuarios.AnyAsync(u => u.Id == req.UsuarioId);
        if (!usuarioExiste)
            return new CriarApostaResult(false, "Usuário não encontrado.", null, null, null);

        var jogo = await db.Jogos.FindAsync(req.JogoId);
        if (jogo is null)
            return new CriarApostaResult(false, "Jogo não encontrado.", null, null, null);

        if (jogo.Status == StatusJogo.Finalizado)
            return new CriarApostaResult(false, "Este jogo já foi finalizado.", null, null, null);

        var statusPagamento = req.FormaPagamento == FormaPagamento.Dinheiro
            ? StatusPagamento.Pago
            : StatusPagamento.Pendente;

        var aposta = new Aposta
        {
            UsuarioId = req.UsuarioId,
            JogoId = req.JogoId,
            GolsTimeA = req.GolsTimeA,
            GolsTimeB = req.GolsTimeB,
            ValorApostado = req.ValorApostado,
            FormaPagamento = req.FormaPagamento,
            StatusPagamento = statusPagamento
        };

        db.Apostas.Add(aposta);
        await db.SaveChangesAsync();

        string? pixCopiaCola = null;
        if (req.FormaPagamento == FormaPagamento.Pix)
            pixCopiaCola = GerarPixCopiaCola(req.ValorApostado);

        return new CriarApostaResult(true, string.Empty, aposta.Id, statusPagamento, pixCopiaCola);
    }

    public async Task<(bool sucesso, string erro)> CancelarAsync(int apostaId)
    {
        var aposta = await db.Apostas.FindAsync(apostaId);
        if (aposta is null)
            return (false, "Aposta não encontrada.");

        if (aposta.StatusPagamento == StatusPagamento.Cancelado)
            return (false, "Aposta já está cancelada.");

        aposta.StatusPagamento = StatusPagamento.Cancelado;
        await db.SaveChangesAsync();

        return (true, string.Empty);
    }

    public async Task<(bool sucesso, string erro)> ConfirmarPagamentoAsync(int apostaId)
    {
        var aposta = await db.Apostas.FindAsync(apostaId);
        if (aposta is null)
            return (false, "Aposta não encontrada.");

        if (aposta.StatusPagamento != StatusPagamento.Pendente)
            return (false, $"Aposta não está pendente (status atual: {aposta.StatusPagamento}).");

        aposta.StatusPagamento = StatusPagamento.Pago;
        await db.SaveChangesAsync();

        return (true, string.Empty);
    }

    private static string GerarPixCopiaCola(decimal valor)
    {
        // Payload simulado — integração real com Mercado Pago substituirá isto
        var valorStr = valor.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        return $"00020126360014BR.GOV.BCB.PIX0114bolao@copa.com5204000053039865406{valorStr}5802BR5909BolaoAPP6009SAO PAULO62070503***6304ABCD";
    }
}
