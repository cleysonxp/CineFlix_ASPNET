namespace CineFlix.API.Response;

public record SerieResponse(
    int Id,
    string Titulo,
    int Temporadas,
    string? Sinopse,
    string Genero
);