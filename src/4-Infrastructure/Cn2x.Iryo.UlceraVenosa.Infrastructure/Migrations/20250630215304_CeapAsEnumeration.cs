using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CeapAsEnumeration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anatomicas");

            migrationBuilder.DropTable(
                name: "Clinicas");

            migrationBuilder.DropTable(
                name: "Etiologicas");

            migrationBuilder.DropTable(
                name: "Fisiologicas");

            migrationBuilder.DropColumn(
                name: "AnatomiaId",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClasseClinicaId",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "EtiologiaId",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "PatofisiologiaId",
                table: "Ulceras");

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_Anatomia",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_ClasseClinica",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_Etiologia",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoCeap_Patofisiologia",
                table: "Ulceras",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_Anatomia",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_ClasseClinica",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_Etiologia",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "ClassificacaoCeap_Patofisiologia",
                table: "Ulceras");

            migrationBuilder.AddColumn<Guid>(
                name: "AnatomiaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClasseClinicaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EtiologiaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatofisiologiaId",
                table: "Ulceras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Anatomicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anatomicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etiologicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiologicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fisiologicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Desativada = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fisiologicas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Anatomicas",
                columns: new[] { "Id", "AtualizadoEm", "Codigo", "CriadoEm", "Desativada", "Descricao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "As", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Sistema superficial" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "Ad", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Sistema profundo" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, "Ap", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Sistema perfurante" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, "An", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Sem localização anatômica identificada" }
                });

            migrationBuilder.InsertData(
                table: "Clinicas",
                columns: new[] { "Id", "AtualizadoEm", "Codigo", "CriadoEm", "Desativada", "Descricao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "C0", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Sem sinais visíveis ou palpáveis de doença venosa" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "C1", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Telangiectasias ou veias reticulares" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, "C2", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Veias varicosas" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, "C3", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Edema" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, "C4a", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Pigmentação ou eczema" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, "C4b", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Lipodermatoesclerose ou atrofia branca" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, "C5", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Úlcera venosa cicatrizada" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), null, "C6", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Úlcera venosa ativa" }
                });

            migrationBuilder.InsertData(
                table: "Etiologicas",
                columns: new[] { "Id", "AtualizadoEm", "Codigo", "CriadoEm", "Desativada", "Descricao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "Ec", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Congênita" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "Ep", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Primária" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, "Es", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Secundária (pós-trombótica)" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, "En", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Não identificada" }
                });

            migrationBuilder.InsertData(
                table: "Fisiologicas",
                columns: new[] { "Id", "AtualizadoEm", "Codigo", "CriadoEm", "Desativada", "Descricao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "Pr", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Refluxo" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "Po", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Obstrução" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, "Pro", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Refluxo e obstrução" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, "Pn", new DateTime(2025, 6, 28, 19, 0, 0, 0, DateTimeKind.Utc), false, "Sem alteração identificada" }
                });
        }
    }
}
