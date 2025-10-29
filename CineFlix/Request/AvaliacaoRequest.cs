using System.ComponentModel.DataAnnotations;

namespace CineFlix.API.Request;

public record AvaliacaoRequest(
    [Required] int Nota,
    string? Comentario,
    bool Assistido,
    [Required] int UsuarioId,
    int? FilmeId,
    int? SerieId
);
