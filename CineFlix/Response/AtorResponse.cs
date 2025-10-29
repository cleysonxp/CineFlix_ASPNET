namespace CineFlix.API.Response;

public record AtorResponse(
    int Id,
    string Nome,
    DateTime? DataNascimento,
    string? Nacionalidade,
    string? FotoPerfil
);
