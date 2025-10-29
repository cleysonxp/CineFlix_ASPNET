using System.ComponentModel.DataAnnotations;

namespace CineFlix.API.Request;

public record SerieRequest(
    [Required] string Titulo,
    [Required] int Temporadas,
    string? Sinopse,
    [Required] int GeneroId
);
