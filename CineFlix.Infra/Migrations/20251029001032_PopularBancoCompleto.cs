using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineFlix.Infra.Migrations
{
    public partial class PopularBancoCompleto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ---------- GÊNEROS ----------
            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Nome" },
                values: new object[,]
                {
                    { "Ação" },
                    { "Comédia" },
                    { "Drama" },
                    { "Ficção Científica" },
                    { "Terror" },
                    { "Romance" },
                    { "Aventura" },
                    { "Suspense" },
                    { "Fantasia" },
                    { "Animação" }
                }
            );

            // ---------- ATORES ----------
            migrationBuilder.InsertData(
                table: "Atores",
                columns: new[] { "Nome", "DataNascimento", "Nacionalidade" },
                values: new object[,]
                {
                    { "Robert Downey Jr.", new DateTime(1965, 4, 4), "Americana" },
                    { "Scarlett Johansson", new DateTime(1984, 11, 22), "Americana" },
                    { "Keanu Reeves", new DateTime(1964, 9, 2), "Canadense" },
                    { "Tom Hanks", new DateTime(1956, 7, 9), "Americana" },
                    { "Morgan Freeman", new DateTime(1937, 6, 1), "Americana" },
                    { "Leonardo DiCaprio", new DateTime(1974, 11, 11), "Americana" },
                    { "Emma Watson", new DateTime(1990, 4, 15), "Britânica" },
                    { "Chris Evans", new DateTime(1981, 6, 13), "Americana" },
                    { "Anne Hathaway", new DateTime(1982, 11, 12), "Americana" },
                    { "Daniel Radcliffe", new DateTime(1989, 7, 23), "Britânica" }
                }
            );

            // ---------- FILMES ----------
            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Titulo", "AnoLancamento", "Sinopse", "GeneroId" },
                values: new object[,]
                {
                    { "Homem de Ferro", 2008, "Um bilionário constrói uma armadura de alta tecnologia e se torna o super-herói Homem de Ferro.", 1 },
                    { "Matrix", 1999, "Um hacker descobre que o mundo é uma simulação controlada por máquinas.", 4 },
                    { "Forrest Gump", 1994, "A vida extraordinária de um homem simples que testemunha eventos marcantes da história americana.", 3 },
                    { "O Senhor dos Anéis: A Sociedade do Anel", 2001, "Um hobbit embarca em uma jornada épica para destruir um anel poderoso.", 9 },
                    { "Titanic", 1997, "Um romance nasce a bordo do Titanic entre classes sociais distintas.", 6 },
                    { "Vingadores: Ultimato", 2019, "Os Vingadores se reúnem para derrotar Thanos e restaurar o equilíbrio do universo.", 1 },
                    { "Harry Potter e a Pedra Filosofal", 2001, "Um jovem bruxo descobre seu destino em uma escola de magia.", 9 },
                    { "O Rei Leão", 1994, "Um leão precisa encontrar seu lugar no ciclo da vida após a perda do pai.", 10 },
                    { "Corra!", 2017, "Um homem negro descobre segredos sombrios na família de sua namorada branca.", 5 },
                    { "Inception", 2010, "Um ladrão invade sonhos para roubar segredos e implantar ideias.", 8 }
                }
            );

            // ---------- SÉRIES ----------
            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Titulo", "Temporadas", "Sinopse", "GeneroId" },
                values: new object[,]
                {
                    { "Breaking Bad", 5, "Um professor de química começa a produzir metanfetamina para sustentar sua família.", 3 },
                    { "Stranger Things", 4, "Um grupo de crianças enfrenta forças sobrenaturais em uma pequena cidade.", 9 },
                    { "The Office", 9, "O cotidiano cômico dos funcionários de uma empresa de papel.", 2 },
                    { "Game of Thrones", 8, "Famílias nobres lutam pelo controle do Trono de Ferro.", 9 },
                    { "Friends", 10, "Um grupo de amigos vive altos e baixos na cidade de Nova York.", 2 },
                    { "Dark", 3, "Uma cidade alemã é abalada pelo desaparecimento de crianças e segredos no tempo.", 8 },
                    { "The Mandalorian", 3, "Um caçador de recompensas navega pelos confins da galáxia após a queda do Império.", 7 },
                    { "Peaky Blinders", 6, "Uma gangue britânica busca poder e controle em Birmingham.", 3 },
                    { "WandaVision", 1, "Wanda Maximoff cria uma realidade alternativa para lidar com sua dor.", 9 },
                    { "Rick and Morty", 6, "Um cientista e seu neto vivem aventuras insanas pelo multiverso.", 10 }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Filmes");
            migrationBuilder.Sql("DELETE FROM Series");
            migrationBuilder.Sql("DELETE FROM Atores");
            migrationBuilder.Sql("DELETE FROM Generos");
        }
    }
}
