namespace CineFlix.API.Request;

public record AtorRequestEdit(
    int Id,
    string Nome,
    DateTime? DataNascimento,
    string? Nacionalidade
) : AtorRequest(Nome, DataNascimento, Nacionalidade, null);
