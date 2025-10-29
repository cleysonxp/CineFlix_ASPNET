namespace CineFlix.API.Request;

public record FilmeRequestEdit(
    int Id,
    string Titulo,
    int AnoLancamento,
    string? Sinopse,
    int GeneroId
) : FilmeRequest(Titulo, AnoLancamento, Sinopse, GeneroId);
