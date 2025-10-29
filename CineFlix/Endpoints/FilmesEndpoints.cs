using CineFlix.API.Request;
using CineFlix.API.Response;
using CineFlix.Domain.Modelo;
using CineFlix.Infra.Banco;
using Microsoft.AspNetCore.Mvc;

namespace CineFlix.API.Endpoints;

public static class FilmesEndpoints
{
    public static void MapFilmesEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("filmes").WithTags("Filmes");

        #region 
        // GET - Listar Filmes
        groupBuilder.MapGet("", ([FromServices] DAL<Filme> dal) =>
        {
            var filmes = dal.Listar()
                .Select(f => new FilmeResponse(
                    f.Id,
                    f.Titulo,
                    f.AnoLancamento,
                    f.Sinopse,
                    f.Genero!.Nome,
                    f.AtoresFilmes?.Select(af => af.Ator!.Nome).ToList()
                ));

            return Results.Ok(filmes);
        });

        // POST - Criar Filme
        groupBuilder.MapPost("", ([FromBody] FilmeRequest request, [FromServices] DAL<Filme> dal) =>
        {
            var novoFilme = new Filme
            {
                Titulo = request.Titulo,
                AnoLancamento = request.AnoLancamento,
                Sinopse = request.Sinopse,
                GeneroId = request.GeneroId
            };

            dal.Adicionar(novoFilme);
            return Results.Created($"/filmes/{novoFilme.Id}", novoFilme);
        });

        // PUT - Editar Filme
        groupBuilder.MapPut("{id:int}", ([FromRoute] int id, [FromBody] FilmeRequestEdit request, [FromServices] DAL<Filme> dal) =>
        {
            var filmeExistente = dal.RecuperarPor(f => f.Id == id);
            if (filmeExistente is null)
            {
                return Results.NotFound($"Filme com ID {id} não encontrado.");
            }

            filmeExistente.Titulo = request.Titulo;
            filmeExistente.AnoLancamento = request.AnoLancamento;
            filmeExistente.Sinopse = request.Sinopse;
            filmeExistente.GeneroId = request.GeneroId;

            dal.Atualizar(filmeExistente);
            return Results.Ok(new FilmeResponse(
                filmeExistente.Id,
                filmeExistente.Titulo,
                filmeExistente.AnoLancamento,
                filmeExistente.Sinopse,
                filmeExistente.Genero!.Nome,
                filmeExistente.AtoresFilmes?.Select(af => af.Ator!.Nome).ToList()
            ));
        });

        // GET - Recuperar por nome
        groupBuilder.MapGet("{titulo}", ([FromServices] DAL<Filme> dal, string titulo) =>
        {
            var filme = dal.RecuperarPor(f => f.Titulo.ToUpper().Equals(titulo.ToUpper()));

            if (filme is null)
                return Results.NotFound($"Filme com título '{titulo}' não foi encontrado.");

            var filmeResponse = new FilmeResponse(
                filme.Id,
                filme.Titulo,
                filme.AnoLancamento,
                filme.Sinopse,
                filme.Genero!.Nome,
                filme.AtoresFilmes?.Select(af => af.Ator!.Nome).ToList()
            );

            return Results.Ok(filmeResponse);
        });

        // DELETE - Remover filme por ID
        groupBuilder.MapDelete("{id:int}", ([FromRoute] int id, [FromServices] DAL<Filme> dal) =>
        {
            var filme = dal.RecuperarPor(f => f.Id == id);

            if (filme is null)
                return Results.NotFound($"Filme com ID {id} não foi encontrado.");

            dal.Deletar(filme);
            return Results.Ok($"Filme '{filme.Titulo}' removido com sucesso!");
        });
        #endregion

    }
}
