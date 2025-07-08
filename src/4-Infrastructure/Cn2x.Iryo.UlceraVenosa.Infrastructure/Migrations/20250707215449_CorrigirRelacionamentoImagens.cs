using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamentoImagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_avaliacoes_ulcera_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_ulceras_UlceraId1",
                table: "imagem_ulcera");

            migrationBuilder.DropIndex(
                name: "IX_imagem_ulcera_UlceraId1",
                table: "imagem_ulcera");

            migrationBuilder.DropColumn(
                name: "UlceraId1",
                table: "imagem_ulcera");

            migrationBuilder.AddColumn<Guid>(
                name: "avaliacao_ulcera_id",
                table: "imagem_ulcera",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_imagem_ulcera_avaliacao_ulcera_id",
                table: "imagem_ulcera",
                column: "avaliacao_ulcera_id");

            migrationBuilder.AddForeignKey(
                name: "FK_imagem_ulcera_avaliacoes_ulcera_avaliacao_ulcera_id",
                table: "imagem_ulcera",
                column: "avaliacao_ulcera_id",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_imagem_ulcera_ulceras_ulcera_id",
                table: "imagem_ulcera",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_avaliacoes_ulcera_avaliacao_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_ulceras_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropIndex(
                name: "IX_imagem_ulcera_avaliacao_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropColumn(
                name: "avaliacao_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.AddColumn<Guid>(
                name: "UlceraId1",
                table: "imagem_ulcera",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_imagem_ulcera_UlceraId1",
                table: "imagem_ulcera",
                column: "UlceraId1");

            migrationBuilder.AddForeignKey(
                name: "FK_imagem_ulcera_avaliacoes_ulcera_ulcera_id",
                table: "imagem_ulcera",
                column: "ulcera_id",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_imagem_ulcera_ulceras_UlceraId1",
                table: "imagem_ulcera",
                column: "UlceraId1",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
