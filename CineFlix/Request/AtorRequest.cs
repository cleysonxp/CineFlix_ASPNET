using System.ComponentModel.DataAnnotations;

namespace CineFlix.API.Request;

public record AtorRequest(
    [Required] string Nome,
    DateTime? DataNascimento,
    string? Nacionalidade,
    string? FotoPerfil // Base64 opcional
);
