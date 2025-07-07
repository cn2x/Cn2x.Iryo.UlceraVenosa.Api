using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAvaliacaoUlceraAndExsudatoDaAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caracteristicas_ulceras_UlceraId",
                table: "caracteristicas");

            migrationBuilder.DropForeignKey(
                name: "FK_ceap_ulceras_UlceraId",
                table: "ceap");

            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_ulceras_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropForeignKey(
                name: "FK_sinais_inflamatorios_ulceras_UlceraId",
                table: "sinais_inflamatorios");

            migrationBuilder.DropColumn(
                name: "data_exame",
                table: "ulceras");

            migrationBuilder.DropColumn(
                name: "duracao",
                table: "ulceras");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "sinais_inflamatorios",
                newName: "AvaliacaoUlceraId");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "ceap",
                newName: "AvaliacaoUlceraId");

            migrationBuilder.RenameColumn(
                name: "UlceraId",
                table: "caracteristicas",
                newName: "AvaliacaoUlceraId");

            migrationBuilder.AddColumn<Guid>(
                name: "UlceraId1",
                table: "imagem_ulcera",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "avaliacoes_ulcera",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_avaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duracao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avaliacoes_ulcera", x => x.id);
                    table.ForeignKey(
                        name: "FK_avaliacoes_ulcera_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exsudatos_avaliacao",
                columns: table => new
                {
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exsudato_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exsudatos_avaliacao", x => new { x.avaliacao_ulcera_id, x.exsudato_id });
                    table.ForeignKey(
                        name: "FK_exsudatos_avaliacao_avaliacoes_ulcera_avaliacao_ulcera_id",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exsudatos_avaliacao_exsudatos_exsudato_id",
                        column: x => x.exsudato_id,
                        principalTable: "exsudatos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_imagem_ulcera_UlceraId1",
                table: "imagem_ulcera",
                column: "UlceraId1");

            migrationBuilder.CreateIndex(
                name: "ix_avaliacoes_ulcera_ulcera_id",
                table: "avaliacoes_ulcera",
                column: "ulcera_id");

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_avaliacao_exsudato_id",
                table: "exsudatos_avaliacao",
                column: "exsudato_id");

            migrationBuilder.AddForeignKey(
                name: "FK_caracteristicas_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "caracteristicas",
                column: "AvaliacaoUlceraId",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ceap_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "ceap",
                column: "AvaliacaoUlceraId",
                principalTable: "avaliacoes_ulcera",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_medidas_avaliacoes_ulcera_ulcera_id",
                table: "medidas",
                column: "ulcera_id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_caracteristicas_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "caracteristicas");

            migrationBuilder.DropForeignKey(
                name: "FK_ceap_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "ceap");

            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_avaliacoes_ulcera_ulcera_id",
                table: "imagem_ulcera");

            migrationBuilder.DropForeignKey(
                name: "FK_imagem_ulcera_ulceras_UlceraId1",
                table: "imagem_ulcera");

            migrationBuilder.DropForeignKey(
                name: "FK_medidas_avaliacoes_ulcera_ulcera_id",
                table: "medidas");

            migrationBuilder.DropForeignKey(
                name: "FK_sinais_inflamatorios_avaliacoes_ulcera_AvaliacaoUlceraId",
                table: "sinais_inflamatorios");

            migrationBuilder.DropTable(
                name: "exsudatos_avaliacao");

            migrationBuilder.DropTable(
                name: "avaliacoes_ulcera");

            migrationBuilder.DropIndex(
                name: "IX_imagem_ulcera_UlceraId1",
                table: "imagem_ulcera");

            migrationBuilder.DropColumn(
                name: "UlceraId1",
                table: "imagem_ulcera");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoUlceraId",
                table: "sinais_inflamatorios",
                newName: "UlceraId");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoUlceraId",
                table: "ceap",
                newName: "UlceraId");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoUlceraId",
                table: "caracteristicas",
                newName: "UlceraId");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_exame",
                table: "ulceras",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "duracao",
                table: "ulceras",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_caracteristicas_ulceras_UlceraId",
                table: "caracteristicas",
                column: "UlceraId",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ceap_ulceras_UlceraId",
                table: "ceap",
                column: "UlceraId",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_imagem_ulcera_ulceras_ulcera_id",
                table: "imagem_ulcera",
                column: "ulcera_id",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sinais_inflamatorios_ulceras_UlceraId",
                table: "sinais_inflamatorios",
                column: "UlceraId",
                principalTable: "ulceras",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
