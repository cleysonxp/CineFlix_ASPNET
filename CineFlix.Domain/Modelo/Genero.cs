using System.ComponentModel.DataAnnotations;

namespace CineFlix.Domain.Modelo;

public class Genero
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nome { get; set; } = string.Empty;

    public virtual ICollection<Filme> Filmes { get; set; }
    public virtual ICollection<Serie> Series { get; set; }

}
