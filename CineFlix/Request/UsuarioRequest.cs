using System.ComponentModel.DataAnnotations;

namespace CineFlix.API.Request;

public record UsuarioRequest(
    [Required] string Nome,
    [Required, EmailAddress] string Email,
    [Required] string Senha
);
