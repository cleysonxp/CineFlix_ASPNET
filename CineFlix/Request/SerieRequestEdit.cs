namespace CineFlix.API.Request;

public record SerieRequestEdit(
    int Id,
    string Titulo,
    int Temporadas,
    string? Sinopse,
    int GeneroId
) : SerieRequest(Titulo, Temporadas, Sinopse, GeneroId);
