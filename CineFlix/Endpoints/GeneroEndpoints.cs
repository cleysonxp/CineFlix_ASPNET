using CineFlix.API.Request;
using CineFlix.API.Response;
using CineFlix.Domain.Modelo;
using CineFlix.Infra.Banco;
using Microsoft.AspNetCore.Mvc;

namespace CineFlix.API.Endpoints;

public static class GenerosEndpoints
{
    public static void MapGenerosEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("generos").WithTags("Gêneros");

        #region
        // GET - Listar todos os gêneros
        groupBuilder.MapGet("", ([FromServices] DAL<Genero> dal) =>
        {
            var generos = dal.Listar()
                .Select(g => new GeneroResponse(g.Id, g.Nome));
            return Results.Ok(generos);
        });

        // GET - Buscar gênero por nome
        groupBuilder.MapGet("{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
        {
            var genero = dal.RecuperarPor(g => g.Nome.ToUpper().Equals(nome.ToUpper()));
            if (genero is null)
                return Results.NotFound($"Gênero '{nome}' não encontrado.");

            return Results.Ok(new GeneroResponse(genero.Id, genero.Nome));
        });

        // POST - Criar novo gênero
        groupBuilder.MapPost("", ([FromBody] GeneroRequest request, [FromServices] DAL<Genero> dal) =>
        {
            var novoGenero = new Genero
            {
                Nome = request.Nome
            };

            dal.Adicionar(novoGenero);
            return Results.Created($"/generos/{novoGenero.Id}", novoGenero);
        });

        // PUT - Atualizar gênero existente
        groupBuilder.MapPut("{id:int}", ([FromRoute] int id, [FromBody] GeneroRequestEdit request, [FromServices] DAL<Genero> dal) =>
        {
            var generoExistente = dal.RecuperarPor(g => g.Id == id);
            if (generoExistente is null)
                return Results.NotFound($"Gênero com ID {id} não encontrado.");

            generoExistente.Nome = request.Nome;
            dal.Atualizar(generoExistente);

            return Results.Ok(new GeneroResponse(generoExistente.Id, generoExistente.Nome));
        });

        // DELETE - Remover gênero por ID
        groupBuilder.MapDelete("{id:int}", ([FromRoute] int id, [FromServices] DAL<Genero> dal) =>
        {
            var genero = dal.RecuperarPor(g => g.Id == id);
            if (genero is null)
                return Results.NotFound($"Gênero com ID {id} não foi encontrado.");

            dal.Deletar(genero);
            return Results.Ok($"Gênero '{genero.Nome}' removido com sucesso!");
        });
        #endregion
    }
}
