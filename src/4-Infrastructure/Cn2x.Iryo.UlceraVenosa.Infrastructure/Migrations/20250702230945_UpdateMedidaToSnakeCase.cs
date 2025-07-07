using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedidaToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medidas_ulceras_ulcera_id",
                table: "Medidas");

            migrationBuilder.RenameTable(
                name: "Medidas",
                newName: "medidas");

            migrationBuilder.AddForeignKey(
                name: "FK_medidas_ulceras_ulcera_id",
                table: "medidas",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medidas_ulceras_ulcera_id",
                table: "medidas");

            migrationBuilder.RenameTable(
                name: "medidas",
                newName: "Medidas");

            migrationBuilder.AddForeignKey(
                name: "FK_Medidas_ulceras_ulcera_id",
                table: "Medidas",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
