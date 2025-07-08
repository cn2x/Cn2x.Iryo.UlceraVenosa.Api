using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCeapValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ceap_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "ceap");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoUlceraId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ceap_ulceras_UlceraId",
                table: "ceap");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "ceap",
                newName: "AvaliacaoUlceraId");

            migrationBuilder.AddForeignKey(
                name: "FK_ceap_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "ceap",
                column: "AvaliacaoUlceraId",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
