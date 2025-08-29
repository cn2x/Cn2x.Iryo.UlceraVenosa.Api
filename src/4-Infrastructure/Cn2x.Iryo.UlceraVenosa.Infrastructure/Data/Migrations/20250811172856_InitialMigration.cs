using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE sinais_inflamatorios ALTER COLUMN dor TYPE integer USING CASE WHEN dor THEN 1 ELSE 0 END;");
            migrationBuilder.Sql("ALTER TABLE sinais_inflamatorios ALTER COLUMN dor DROP NOT NULL;");

            migrationBuilder.AddColumn<Guid>(
                name: "profissional_id",
                table: "avaliacoes_ulcera",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "profissionais",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    desativada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profissionais", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_avaliacoes_ulcera_profissional_id",
                table: "avaliacoes_ulcera",
                column: "profissional_id");

            migrationBuilder.CreateIndex(
                name: "IX_profissionais_nome",
                table: "profissionais",
                column: "nome");

            migrationBuilder.AddForeignKey(
                name: "FK_avaliacoes_ulcera_profissionais_profissional_id",
                table: "avaliacoes_ulcera",
                column: "profissional_id",
                principalTable: "profissionais",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_avaliacoes_ulcera_profissionais_profissional_id",
                table: "avaliacoes_ulcera");

            migrationBuilder.DropTable(
                name: "profissionais");

            migrationBuilder.DropIndex(
                name: "ix_avaliacoes_ulcera_profissional_id",
                table: "avaliacoes_ulcera");

            migrationBuilder.DropColumn(
                name: "profissional_id",
                table: "avaliacoes_ulcera");

            migrationBuilder.AlterColumn<bool>(
                name: "dor",
                table: "sinais_inflamatorios",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
