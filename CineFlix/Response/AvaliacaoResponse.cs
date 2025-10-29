namespace CineFlix.API.Response;

public record AvaliacaoResponse(
    int Id,
    int Nota,
    string? Comentario,
    bool Assistido,
    DateTime DataAvaliacao,
    string Usuario,
    string? Filme,
    string? Serie
);