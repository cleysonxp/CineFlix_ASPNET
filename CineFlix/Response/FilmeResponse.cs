namespace CineFlix.API.Response;

public record FilmeResponse(
    int Id,
    string Titulo,
    int AnoLancamento,
    string? Sinopse,
    string Genero,
    List<string>? Atores
);