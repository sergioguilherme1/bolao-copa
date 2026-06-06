namespace BolaoCopa.API.Models;

public enum StatusJogo
{
    Pendente,
    Finalizado
}

public class Jogo
{
    public int Id { get; set; }
    public string TimeA { get; set; } = string.Empty;
    public string TimeB { get; set; } = string.Empty;
    public int? GolsTimeA { get; set; }
    public int? GolsTimeB { get; set; }
    public StatusJogo Status { get; set; } = StatusJogo.Pendente;

    public ICollection<Aposta> Apostas { get; set; } = [];
}
