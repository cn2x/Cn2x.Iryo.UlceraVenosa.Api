using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExsudatoDaUlceraToCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_exsudatos",
                table: "exsudatos_da_ulcera");

            migrationBuilder.DropIndex(
                name: "ix_exsudatos_ulcera_id",
                table: "exsudatos_da_ulcera");

            migrationBuilder.DropColumn(
                name: "id",
                table: "exsudatos_da_ulcera");

            migrationBuilder.DropColumn(
                name: "atualizado_em",
                table: "exsudatos_da_ulcera");

            migrationBuilder.DropColumn(
                name: "criado_em",
                table: "exsudatos_da_ulcera");

            migrationBuilder.DropColumn(
                name: "desativada",
                table: "exsudatos_da_ulcera");

            migrationBuilder.DropColumn(
                name: "descricao",
                table: "exsudatos_da_ulcera");

            migrationBuilder.AddPrimaryKey(
                name: "PK_exsudatos_da_ulcera",
                table: "exsudatos_da_ulcera",
                columns: new[] { "ulcera_id", "exsudato_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_exsudatos_da_ulcera",
                table: "exsudatos_da_ulcera");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "exsudatos_da_ulcera",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "atualizado_em",
                table: "exsudatos_da_ulcera",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "criado_em",
                table: "exsudatos_da_ulcera",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "desativada",
                table: "exsudatos_da_ulcera",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "descricao",
                table: "exsudatos_da_ulcera",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_exsudatos",
                table: "exsudatos_da_ulcera",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_exsudatos_ulcera_id",
                table: "exsudatos_da_ulcera",
                column: "ulcera_id");
        }
    }
}
