using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineFlix.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFotoPerfilAtor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Atores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Atores");
        }
    }
}
