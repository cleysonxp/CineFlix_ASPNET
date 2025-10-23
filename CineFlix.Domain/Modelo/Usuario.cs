using System.ComponentModel.DataAnnotations;

namespace CineFlix.Domain.Modelo;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string SenhaHash { get; set; } = string.Empty; // senha criptografada

    public virtual ICollection<Avaliacao>? Avaliacoes { get; set; }

}
