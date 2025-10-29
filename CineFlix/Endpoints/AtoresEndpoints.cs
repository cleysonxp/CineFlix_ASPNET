using CineFlix.API.Request;
using CineFlix.API.Response;
using CineFlix.Domain.Modelo;
using CineFlix.Infra.Banco;
using Microsoft.AspNetCore.Mvc;

namespace CineFlix.API.Endpoints;

public static class AtoresEndpoints
{
    public static void MapAtoresEndpoints(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("atores").WithTags("Atores");

        #region
        // GET - Listar todos os atores
        groupBuilder.MapGet("", ([FromServices] DAL<Ator> dal) =>
        {
            var atores = dal.Listar()
                .Select(a => new AtorResponse(
                    a.Id,
                    a.Nome,
                    a.DataNascimento,
                    a.Nacionalidade,
                    a.FotoPerfil
                ));

            return Results.Ok(atores);
        });

        // GET - Buscar ator por nome
        groupBuilder.MapGet("{nome}", ([FromServices] DAL<Ator> dal, string nome) =>
        {
            var ator = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (ator is null)
                return Results.NotFound($"Ator '{nome}' não encontrado.");

            var atorResponse = new AtorResponse(
                ator.Id,
                ator.Nome,
                ator.DataNascimento,
                ator.Nacionalidade,
                ator.FotoPerfil
            );

            return Results.Ok(atorResponse);
        });

        // POST - Criar novo ator (com upload de foto em Base64 opcional)
        groupBuilder.MapPost("", async (
            [FromServices] IHostEnvironment env,
            [FromServices] DAL<Ator> dal,
            [FromBody] AtorRequest request) =>
        {
            string? caminhoFoto = null;

            if (!string.IsNullOrEmpty(request.FotoPerfil))
            {
                // Gera nome do arquivo e caminho físico
                var nomeArquivo = $"{DateTime.Now:yyyyMMddHHmmss}_{request.Nome.Replace(" ", "_")}.jpg";
                var caminho = Path.Combine(env.ContentRootPath, "wwwroot", "FotosAtores", nomeArquivo);

                // Cria a pasta se não existir
                Directory.CreateDirectory(Path.GetDirectoryName(caminho)!);

                // Converte Base64 em imagem física
                using MemoryStream ms = new MemoryStream(Convert.FromBase64String(request.FotoPerfil));
                using FileStream fs = new FileStream(caminho, FileMode.Create);
                await ms.CopyToAsync(fs);

                caminhoFoto = $"/FotosAtores/{nomeArquivo}";
            }

            var novoAtor = new Ator
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                Nacionalidade = request.Nacionalidade,
                FotoPerfil = caminhoFoto
            };

            dal.Adicionar(novoAtor);
            return Results.Created($"/atores/{novoAtor.Id}", novoAtor);
        });

        // PUT - Atualizar ator existente
        groupBuilder.MapPut("{id:int}", ([FromRoute] int id, [FromBody] AtorRequestEdit request, [FromServices] DAL<Ator> dal) =>
        {
            var atorExistente = dal.RecuperarPor(a => a.Id == id);
            if (atorExistente is null)
                return Results.NotFound($"Ator com ID {id} não encontrado.");

            atorExistente.Nome = request.Nome;
            atorExistente.DataNascimento = request.DataNascimento;
            atorExistente.Nacionalidade = request.Nacionalidade;
            dal.Atualizar(atorExistente);

            return Results.Ok(new AtorResponse(
                atorExistente.Id,
                atorExistente.Nome,
                atorExistente.DataNascimento,
                atorExistente.Nacionalidade,
                atorExistente.FotoPerfil
            ));
        });

        // DELETE - Remover ator por ID
        groupBuilder.MapDelete("{id:int}", ([FromRoute] int id, [FromServices] DAL<Ator> dal) =>
        {
            var ator = dal.RecuperarPor(a => a.Id == id);
            if (ator is null)
                return Results.NotFound($"Ator com ID {id} não foi encontrado.");

            dal.Deletar(ator);
            return Results.Ok($"Ator '{ator.Nome}' removido com sucesso!");
        });
        #endregion
    }
}
