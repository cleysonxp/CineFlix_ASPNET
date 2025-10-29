using System.ComponentModel.DataAnnotations;

namespace CineFlix.API.Request;

public record GeneroRequest(
    [Required] string Nome
);
