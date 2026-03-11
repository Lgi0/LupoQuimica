using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LupoQuimica.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarMarca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrincipioAtivo",
                table: "Produtos",
                newName: "Marca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Produtos",
                newName: "PrincipioAtivo");
        }
    }
}
