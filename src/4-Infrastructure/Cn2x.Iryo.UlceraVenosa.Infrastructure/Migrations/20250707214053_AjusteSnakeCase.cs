using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ceap_ulceras_UlceraId",
                table: "ceap");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "ceap",
                newName: "ulcera_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ceap_ulceras_ulcera_id",
                table: "ceap",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ceap_ulceras_ulcera_id",
                table: "ceap");

            migrationBuilder.RenameColumn(
                name: "ulcera_id",
                table: "ceap",
                newName: "UlceraId");

            migrationBuilder.AddForeignKey(
                name: "FK_ceap_ulceras_UlceraId",
                table: "ceap",
                column: "UlceraId",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
