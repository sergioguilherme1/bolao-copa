using BolaoCopa.API.Data;
using BolaoCopa.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolaoCopa.API.Services;

public record GanhadorResult(int UsuarioId, string NomeUsuario, string ChavePix, decimal ValorAReceber);

public record ApuracaoResult(
    bool Sucesso,
    string Erro,
    bool Acumulou,
    decimal PoteTotal,
    decimal SaldoAcumuladoAtualizado,
    IReadOnlyList<GanhadorResult> Ganhadores);

public class ApuracaoService(BolaoDbContext db)
{
    public async Task<ApuracaoResult> FinalizarJogoAsync(int jogoId, int golsTimeA, int golsTimeB)
    {
        var jogo = await db.Jogos.FindAsync(jogoId);
        if (jogo is null)
            return Erro("Jogo não encontrado.");

        if (jogo.Status == StatusJogo.Finalizado)
            return Erro("Jogo já foi finalizado.");

        var apostas = await db.Apostas
            .Include(a => a.Usuario)
            .Where(a => a.JogoId == jogoId)
            .ToListAsync();

        var caixa = await db.CaixaBolao.FindAsync(1);
        if (caixa is null)
            return Erro("CaixaBolao não encontrada.");

        var apostasPageas = apostas.Where(a => a.StatusPagamento == StatusPagamento.Pago).ToList();
        var totalDoJogo = apostasPageas.Sum(a => a.ValorApostado);
        var poteTotal = caixa.SaldoAcumulado + totalDoJogo;

        var acertadores = apostasPageas
            .Where(a => a.GolsTimeA == golsTimeA && a.GolsTimeB == golsTimeB)
            .ToList();

        jogo.GolsTimeA = golsTimeA;
        jogo.GolsTimeB = golsTimeB;
        jogo.Status = StatusJogo.Finalizado;

        ApuracaoResult resultado;

        if (acertadores.Count == 0)
        {
            caixa.SaldoAcumulado = poteTotal;
            resultado = new ApuracaoResult(
                Sucesso: true,
                Erro: string.Empty,
                Acumulou: true,
                PoteTotal: poteTotal,
                SaldoAcumuladoAtualizado: poteTotal,
                Ganhadores: []);
        }
        else
        {
            // Arredondamento para baixo em 2 casas decimais evita perda de centavos
            var valorPorGanhador = Math.Floor(poteTotal / acertadores.Count * 100) / 100;
            caixa.SaldoAcumulado = 0;

            var ganhadores = acertadores
                .Select(a => new GanhadorResult(
                    a.UsuarioId,
                    a.Usuario.Nome,
                    a.Usuario.ChavePix,
                    valorPorGanhador))
                .ToList();

            resultado = new ApuracaoResult(
                Sucesso: true,
                Erro: string.Empty,
                Acumulou: false,
                PoteTotal: poteTotal,
                SaldoAcumuladoAtualizado: 0,
                Ganhadores: ganhadores);
        }

        await db.SaveChangesAsync();
        return resultado;
    }

    private static ApuracaoResult Erro(string mensagem) =>
        new(false, mensagem, false, 0, 0, []);
}
