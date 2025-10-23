using CineFlix.Domain.Modelo;
using CineFlix.Infra.Banco;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ?? Configuração do banco de dados (Entity Framework)
builder.Services.AddDbContext<CineFlixContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CineFlixDB"))
           .UseLazyLoadingProxies();
});

// ?? Adiciona Controllers e configura o JSON para evitar referência circular
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ?? Registra o DAL genérico
builder.Services.AddTransient<DAL<Filme>>();
builder.Services.AddTransient<DAL<Ator>>();
builder.Services.AddTransient<DAL<Genero>>();
builder.Services.AddTransient<DAL<Serie>>();
builder.Services.AddTransient<DAL<Avaliacao>>();
builder.Services.AddTransient<DAL<Usuario>>();

var app = builder.Build();

// ?? Configuração de ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
