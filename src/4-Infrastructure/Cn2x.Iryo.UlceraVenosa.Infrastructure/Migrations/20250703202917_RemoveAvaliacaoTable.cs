using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAvaliacaoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ulceras_avaliacoes_avaliacao_id",
                table: "ulceras");

            migrationBuilder.DropTable(
                name: "avaliacoes");

            migrationBuilder.DropIndex(
                name: "ix_ulceras_avaliacao_id",
                table: "ulceras");

            migrationBuilder.DropColumn(
                name: "avaliacao_id",
                table: "ulceras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "avaliacao_id",
                table: "ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "avaliacoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    paciente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    conduta = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_avaliacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    desativada = table.Column<bool>(type: "boolean", nullable: false),
                    diagnostico = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    observacoes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avaliacoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_avaliacoes_pacientes_paciente_id",
                        column: x => x.paciente_id,
                        principalTable: "pacientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ulceras_avaliacao_id",
                table: "ulceras",
                column: "avaliacao_id");

            migrationBuilder.CreateIndex(
                name: "ix_avaliacoes_paciente_id",
                table: "avaliacoes",
                column: "paciente_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ulceras_avaliacoes_avaliacao_id",
                table: "ulceras",
                column: "avaliacao_id",
                principalTable: "avaliacoes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
