using System.ComponentModel.DataAnnotations;

namespace CineFlix.Domain.Modelo;

public class Filme
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Titulo { get; set; } = string.Empty;

    public int AnoLancamento { get; set; }

    public string? Sinopse { get; set; }

    // 🔹 Relacionamento com Gênero (1:N)
    public int GeneroId { get; set; }
    public virtual Genero? Genero { get; set; }

    // 🔹 Relacionamentos N:N e 1:N
    public virtual ICollection<AtorFilme>? AtoresFilmes { get; set; }

    public virtual ICollection<Avaliacao>? Avaliacoes { get; set; }
}
