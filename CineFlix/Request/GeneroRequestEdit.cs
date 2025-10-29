namespace CineFlix.API.Request;

public record GeneroRequestEdit(
    int Id,
    string Nome
) : GeneroRequest(Nome);
