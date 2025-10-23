using CineFlix.Domain.Modelo;
using Microsoft.EntityFrameworkCore;

namespace CineFlix.Infra.Banco
{
    public class CineFlixContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<AtorFilme> AtoresFilmes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CineFlixDB;Integrated Security=True;Encrypt=False;";

        public CineFlixContext() { }

        public CineFlixContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_connectionString)
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Tabela de junção N:N entre Filme e Ator
            modelBuilder.Entity<AtorFilme>()
                .HasKey(af => new { af.AtorId, af.FilmeId });

            modelBuilder.Entity<AtorFilme>()
                .HasOne(af => af.Ator)
                .WithMany(a => a.AtoresFilmes)
                .HasForeignKey(af => af.AtorId);

            modelBuilder.Entity<AtorFilme>()
                .HasOne(af => af.Filme)
                .WithMany(f => f.AtoresFilmes)
                .HasForeignKey(af => af.FilmeId);

            // 🔹 Índice único para o e-mail do usuário
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 🔹 Relacionamento 1:N entre Genero e Filme
            modelBuilder.Entity<Filme>()
                .HasOne(f => f.Genero)
                .WithMany(g => g.Filmes)
                .HasForeignKey(f => f.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relacionamento 1:N entre Genero e Serie
            modelBuilder.Entity<Serie>()
                .HasOne(s => s.Genero)
                .WithMany(g => g.Series)
                .HasForeignKey(s => s.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relacionamento de Avaliação (Usuario → Filme/Serie)
            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Filme)
                .WithMany(f => f.Avaliacoes)
                .HasForeignKey(a => a.FilmeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Serie)
                .WithMany(s => s.Avaliacoes)
                .HasForeignKey(a => a.SerieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
