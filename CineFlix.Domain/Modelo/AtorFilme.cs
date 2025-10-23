namespace CineFlix.Domain.Modelo;

public class AtorFilme
{
    public int AtorId { get; set; }
    public int FilmeId { get; set; }

    public virtual Ator? Ator { get; set; }
    public virtual Filme? Filme { get; set; }
}
