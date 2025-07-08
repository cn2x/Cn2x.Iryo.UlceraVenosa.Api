using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteImagemAvaliacaoUlcera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagem_ulcera");

            migrationBuilder.CreateTable(
                name: "imagens_avaliacao_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imagem_avaliacao_ulcera", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagens_avaliacao_ulcera_avaliacoes_ulcera_avaliacao_ulcera~",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "imagem",
                columns: table => new
                {
                    ImagemAvaliacaoUlceraId = table.Column<Guid>(type: "uuid", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    tamanho_bytes = table.Column<long>(type: "bigint", nullable: false),
                    data_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "ix_imagem_avaliacao_ulcera_avaliacao_ulcera_id",
                table: "imagens_avaliacao_ulcera",
                column: "avaliacao_ulcera_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagem");

            migrationBuilder.DropTable(
                name: "imagens_avaliacao_ulcera");

            migrationBuilder.CreateTable(
                name: "imagem_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    caminho_arquivo = table.Column<string>(type: "text", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    desativada = table.Column<bool>(type: "boolean", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    eh_imagem_principal = table.Column<bool>(type: "boolean", nullable: false),
                    nome_arquivo = table.Column<string>(type: "text", nullable: false),
                    observacoes = table.Column<string>(type: "text", nullable: true),
                    ordem_exibicao = table.Column<int>(type: "integer", nullable: false),
                    tamanho_bytes = table.Column<long>(type: "bigint", nullable: false),
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imagem_ulcera", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagem_ulcera_avaliacoes_ulcera_avaliacao_ulcera_id",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_imagem_ulcera_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_imagem_ulcera_avaliacao_ulcera_id",
                table: "imagem_ulcera",
                column: "avaliacao_ulcera_id");

            migrationBuilder.CreateIndex(
                name: "ix_imagem_ulcera_ulcera_id",
                table: "imagem_ulcera",
                column: "ulcera_id");
        }
    }
}
