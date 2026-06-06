using BolaoCopa.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolaoCopa.API.Data;

public class BolaoDbContext(DbContextOptions<BolaoDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<Aposta> Apostas => Set<Aposta>();
    public DbSet<CaixaBolao> CaixaBolao => Set<CaixaBolao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Nome).IsRequired().HasMaxLength(150);
            e.Property(u => u.Telefone).IsRequired().HasMaxLength(20);
            e.Property(u => u.ChavePix).IsRequired().HasMaxLength(150);
            e.HasIndex(u => u.Telefone).IsUnique();
        });

        modelBuilder.Entity<Jogo>(e =>
        {
            e.HasKey(j => j.Id);
            e.Property(j => j.TimeA).IsRequired().HasMaxLength(100);
            e.Property(j => j.TimeB).IsRequired().HasMaxLength(100);
            e.Property(j => j.Status).HasConversion<string>();
        });

        modelBuilder.Entity<Aposta>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.ValorApostado).HasPrecision(18, 2);
            e.Property(a => a.FormaPagamento).HasConversion<string>();
            e.Property(a => a.StatusPagamento).HasConversion<string>();
            e.HasOne(a => a.Usuario).WithMany(u => u.Apostas).HasForeignKey(a => a.UsuarioId);
            e.HasOne(a => a.Jogo).WithMany(j => j.Apostas).HasForeignKey(a => a.JogoId);
        });

        modelBuilder.Entity<CaixaBolao>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.SaldoAcumulado).HasPrecision(18, 2);
            e.HasData(new CaixaBolao { Id = 1, SaldoAcumulado = 0 });
        });
    }
}
