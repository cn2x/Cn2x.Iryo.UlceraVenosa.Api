using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMedidaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comprimento_cm",
                table: "ulceras");

            migrationBuilder.DropColumn(
                name: "largura",
                table: "ulceras");

            migrationBuilder.DropColumn(
                name: "profundidade",
                table: "ulceras");

            migrationBuilder.CreateTable(
                name: "Medidas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprimento = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    largura = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    profundidade = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Medidas_ulceras_ulcera_id",
                        column: x => x.ulcera_id,
                        principalTable: "ulceras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_medidas_ulcera_id",
                table: "Medidas",
                column: "ulcera_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medidas");

            migrationBuilder.AddColumn<decimal>(
                name: "comprimento_cm",
                table: "ulceras",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "largura",
                table: "ulceras",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "profundidade",
                table: "ulceras",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
