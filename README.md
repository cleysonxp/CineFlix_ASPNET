# 🎬 CineFlix API

API REST desenvolvida em ASP.NET Core para gerenciamento de filmes e séries.

## 🚀 Sobre o projeto

O CineFlix é uma API REST desenvolvida com ASP.NET Core para gerenciamento
de um catálogo de filmes e séries, permitindo operações sobre filmes,
séries, atores e gêneros.

O projeto foi desenvolvido com uma arquitetura separada em camadas,
buscando manter as responsabilidades organizadas entre API, domínio e
infraestrutura.

## 🛠️ Tecnologias

- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## 📁 Estrutura

```text
CineFlix
├── CineFlix.API
│   ├── Endpoints
│   ├── Request
│   └── Response
│
├── CineFlix.Domain
│   └── Modelo
│
└── CineFlix.Infra
    ├── Banco
    └── Migrations
