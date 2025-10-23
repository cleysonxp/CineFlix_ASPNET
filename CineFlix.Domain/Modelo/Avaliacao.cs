using System.ComponentModel.DataAnnotations;

namespace CineFlix.Domain.Modelo;

public class Avaliacao
{
    [Key]
    public int Id { get; set; }

    [Range(0, 10)]
    public int Nota { get; set; }

    [MaxLength(500)]
    public string? Comentario { get; set; }

    public bool Assistido { get; set; } = false;

    public DateTime DataAvaliacao { get; set; } = DateTime.Now;

    // 🔹 Chaves estrangeiras
    public int UsuarioId { get; set; }
    public int? FilmeId { get; set; }
    public int? SerieId { get; set; }

    // 🔹 Navegações virtuais (Lazy Loading)
    public virtual Usuario? Usuario { get; set; }
    public virtual Filme? Filme { get; set; }
    public virtual Serie? Serie { get; set; }
}
