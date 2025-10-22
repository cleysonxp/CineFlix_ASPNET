using CineFlix.Domain.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

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
        }
    }
}
