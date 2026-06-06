namespace BolaoCopa.API.Models;

public enum FormaPagamento
{
    Pix,
    Dinheiro
}

public enum StatusPagamento
{
    Pendente,
    Pago,
    Cancelado
}

public class Aposta
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int JogoId { get; set; }
    public int GolsTimeA { get; set; }
    public int GolsTimeB { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public StatusPagamento StatusPagamento { get; set; } = StatusPagamento.Pendente;
    public decimal ValorApostado { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public Jogo Jogo { get; set; } = null!;
}
