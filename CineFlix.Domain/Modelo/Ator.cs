using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFlix.Domain.Modelo;

public class Ator
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    public DateTime? DataNascimento { get; set; }

    [MaxLength(50)]
    public string? Nacionalidade { get; set; }

    // Relacionamento N:N com Filme
    public virtual ICollection<AtorFilme>? AtoresFilmes { get; set; }
}
