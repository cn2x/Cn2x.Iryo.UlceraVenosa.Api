using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLateralidadeValueConverter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lado",
                table: "Topografias");

            migrationBuilder.AddColumn<int>(
                name: "Lado",
                table: "Topografias",
                type: "integer",
                nullable: false,
                defaultValue: 1 // Valor padrão para Direta
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lado",
                table: "Topografias");

            migrationBuilder.AddColumn<string>(
                name: "Lado",
                table: "Topografias",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Direta"
            );
        }
    }
}
