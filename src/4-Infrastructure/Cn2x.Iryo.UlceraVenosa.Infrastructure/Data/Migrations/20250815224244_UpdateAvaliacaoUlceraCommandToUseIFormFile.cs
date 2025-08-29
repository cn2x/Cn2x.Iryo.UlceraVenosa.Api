using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvaliacaoUlceraCommandToUseIFormFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagem");

            migrationBuilder.AddColumn<Guid>(
                name: "imagem_id",
                table: "imagens_avaliacao_ulcera",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "imagens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    data_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imagem", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_imagens_avaliacao_ulcera_imagem_id",
                table: "imagens_avaliacao_ulcera",
                column: "imagem_id");

            migrationBuilder.AddForeignKey(
                name: "FK_imagens_avaliacao_ulcera_imagens_imagem_id",
                table: "imagens_avaliacao_ulcera",
                column: "imagem_id",
                principalTable: "imagens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_imagens_avaliacao_ulcera_imagens_imagem_id",
                table: "imagens_avaliacao_ulcera");

            migrationBuilder.DropTable(
                name: "imagens");

            migrationBuilder.DropIndex(
                name: "IX_imagens_avaliacao_ulcera_imagem_id",
                table: "imagens_avaliacao_ulcera");

            migrationBuilder.DropColumn(
                name: "imagem_id",
                table: "imagens_avaliacao_ulcera");

            migrationBuilder.CreateTable(
                name: "imagem",
                columns: table => new
                {
                    ImagemAvaliacaoUlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    data_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tamanho_bytes = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagem", x => x.ImagemAvaliacaoUlceraId);
                    table.ForeignKey(
                        name: "FK_imagem_imagens_avaliacao_ulcera_ImagemAvaliacaoUlceraId",
                        column: x => x.ImagemAvaliacaoUlceraId,
                        principalTable: "imagens_avaliacao_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
