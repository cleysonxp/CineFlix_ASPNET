using CineFlix.API.Request;
using CineFlix.API.Response;
using CineFlix.Domain.Modelo;
using CineFlix.Infra.Banco;
using Microsoft.AspNetCore.Mvc;

namespace CineFlix.API.Endpoints;

public static class SeriesEndpoints
{
    public static void MapSeriesEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("series").WithTags("Séries");

        #region
        // GET - Listar todas as séries
        groupBuilder.MapGet("", ([FromServices] DAL<Serie> dal) =>
        {
            var series = dal.Listar()
                .Select(s => new SerieResponse(
                    s.Id,
                    s.Titulo,
                    s.Temporadas,
                    s.Sinopse,
                    s.Genero!.Nome
                ));
            return Results.Ok(series);
        });

        // GET - Buscar série por título
        groupBuilder.MapGet("{titulo}", ([FromServices] DAL<Serie> dal, string titulo) =>
        {
            var serie = dal.RecuperarPor(s => s.Titulo.ToUpper().Equals(titulo.ToUpper()));
            if (serie is null)
                return Results.NotFound($"Série '{titulo}' não encontrada.");

            var serieResponse = new SerieResponse(
                serie.Id,
                serie.Titulo,
                serie.Temporadas,
                serie.Sinopse,
                serie.Genero!.Nome
            );

            return Results.Ok(serieResponse);
        });

        // POST - Criar nova série
        groupBuilder.MapPost("", ([FromBody] SerieRequest request, [FromServices] DAL<Serie> dal) =>
        {
            var novaSerie = new Serie
            {
                Titulo = request.Titulo,
                Temporadas = request.Temporadas,
                Sinopse = request.Sinopse,
                GeneroId = request.GeneroId
            };

            dal.Adicionar(novaSerie);
            return Results.Created($"/series/{novaSerie.Id}", novaSerie);
        });

        // PUT - Atualizar série existente
        groupBuilder.MapPut("{id:int}", ([FromRoute] int id, [FromBody] SerieRequestEdit request, [FromServices] DAL<Serie> dal) =>
        {
            var serieExistente = dal.RecuperarPor(s => s.Id == id);
            if (serieExistente is null)
                return Results.NotFound($"Série com ID {id} não encontrada.");

            serieExistente.Titulo = request.Titulo;
            serieExistente.Temporadas = request.Temporadas;
            serieExistente.Sinopse = request.Sinopse;
            serieExistente.GeneroId = request.GeneroId;

            dal.Atualizar(serieExistente);

            var serieResponse = new SerieResponse(
                serieExistente.Id,
                serieExistente.Titulo,
                serieExistente.Temporadas,
                serieExistente.Sinopse,
                serieExistente.Genero!.Nome
            );

            return Results.Ok(serieResponse);
        });

        // DELETE - Remover série por ID
        groupBuilder.MapDelete("{id:int}", ([FromRoute] int id, [FromServices] DAL<Serie> dal) =>
        {
            var serie = dal.RecuperarPor(s => s.Id == id);
            if (serie is null)
                return Results.NotFound($"Série com ID {id} não foi encontrada.");

            dal.Deletar(serie);
            return Results.Ok($"Série '{serie.Titulo}' removida com sucesso!");
        });
        #endregion
    }
}
