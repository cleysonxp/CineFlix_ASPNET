namespace CineFlix.API.Response;

public record UsuarioResponse(
    int Id,
    string Nome,
    string Email
);