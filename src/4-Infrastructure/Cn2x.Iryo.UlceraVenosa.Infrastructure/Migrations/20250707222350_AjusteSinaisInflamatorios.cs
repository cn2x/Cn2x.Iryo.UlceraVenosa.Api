using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteSinaisInflamatorios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caracteristicas_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "caracteristicas");

            migrationBuilder.DropForeignKey(
                name: "FK_sinais_inflamatorios_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "sinais_inflamatorios");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoUlceraId",
                table: "sinais_inflamatorios",
                newName: "avaliacao_ferida_id");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoUlceraId",
                table: "caracteristicas",
                newName: "avaliacao_ferida_id");

            migrationBuilder.AddForeignKey(
                name: "FK_caracteristicas_avaliacoes_ulcera_avaliacao_ferida_id",
                table: "caracteristicas",
                column: "avaliacao_ferida_id",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sinais_inflamatorios_avaliacoes_ulcera_avaliacao_ferida_id",
                table: "sinais_inflamatorios",
                column: "avaliacao_ferida_id",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caracteristicas_avaliacoes_ulcera_avaliacao_ferida_id",
                table: "caracteristicas");

            migrationBuilder.DropForeignKey(
                name: "FK_sinais_inflamatorios_avaliacoes_ulcera_avaliacao_ferida_id",
                table: "sinais_inflamatorios");

            migrationBuilder.RenameColumn(
                name: "avaliacao_ferida_id",
                table: "sinais_inflamatorios",
                newName: "AvaliacaoUlceraId");

            migrationBuilder.RenameColumn(
                name: "avaliacao_ferida_id",
                table: "caracteristicas",
                newName: "AvaliacaoUlceraId");

            migrationBuilder.AddForeignKey(
                name: "FK_caracteristicas_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "caracteristicas",
                column: "AvaliacaoUlceraId",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sinais_inflamatorios_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "sinais_inflamatorios",
                column: "AvaliacaoUlceraId",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
