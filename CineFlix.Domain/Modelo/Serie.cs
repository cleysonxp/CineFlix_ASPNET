using System.ComponentModel.DataAnnotations;

namespace CineFlix.Domain.Modelo;

public class Serie
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Titulo { get; set; }
    public int Temporadas { get; set; }
    public string? Sinopse { get; set; }
    public int GeneroId { get; set; }
    public virtual Genero? Genero { get; set; }

    public virtual ICollection<Avaliacao>? Avaliacoes { get; set; }

}
