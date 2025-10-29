using System.ComponentModel.DataAnnotations;

namespace CineFlix.API.Request;

public record FilmeRequest(
    [Required] string Titulo,
    [Required] int AnoLancamento,
    string? Sinopse,
    [Required] int GeneroId
);
