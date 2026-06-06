namespace BolaoCopa.API.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string ChavePix { get; set; } = string.Empty;

    public ICollection<Aposta> Apostas { get; set; } = [];
}
