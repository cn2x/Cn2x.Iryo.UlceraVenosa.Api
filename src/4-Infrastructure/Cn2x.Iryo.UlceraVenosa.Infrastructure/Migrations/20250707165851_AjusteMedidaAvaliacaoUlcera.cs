using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteMedidaAvaliacaoUlcera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medidas_avaliacoes_ulcera_ulcera_id",
                table: "medidas");

            migrationBuilder.DropForeignKey(
                name: "FK_medidas_ulceras_ulcera_id",
                table: "medidas");

            migrationBuilder.DropColumn(
                name: "duracao",
                table: "avaliacoes_ulcera");

            migrationBuilder.RenameColumn(
                name: "ulcera_id",
                table: "medidas",
                newName: "avaliacao_ulcera_id");

            migrationBuilder.RenameIndex(
                name: "ix_medidas_ulcera_id",
                table: "medidas",
                newName: "ix_medidas_avaliacao_ulcera_id");

            migrationBuilder.AddColumn<int>(
                name: "meses_duracao",
                table: "avaliacoes_ulcera",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_medidas_avaliacoes_ulcera_avaliacao_ulcera_id",
                table: "medidas",
                column: "avaliacao_ulcera_id",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medidas_avaliacoes_ulcera_avaliacao_ulcera_id",
                table: "medidas");

            migrationBuilder.DropColumn(
                name: "meses_duracao",
                table: "avaliacoes_ulcera");

            migrationBuilder.RenameColumn(
                name: "avaliacao_ulcera_id",
                table: "medidas",
                newName: "ulcera_id");

            migrationBuilder.RenameIndex(
                name: "ix_medidas_avaliacao_ulcera_id",
                table: "medidas",
                newName: "ix_medidas_ulcera_id");

            migrationBuilder.AddColumn<string>(
                name: "duracao",
                table: "avaliacoes_ulcera",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_medidas_avaliacoes_ulcera_ulcera_id",
                table: "medidas",
                column: "ulcera_id",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_medidas_ulceras_ulcera_id",
                table: "medidas",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
