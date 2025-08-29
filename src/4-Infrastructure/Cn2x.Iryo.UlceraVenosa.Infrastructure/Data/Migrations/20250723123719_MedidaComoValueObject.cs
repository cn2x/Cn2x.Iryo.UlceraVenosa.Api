using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MedidaComoValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medidas");

            migrationBuilder.CreateTable(
                name: "medida",
                columns: table => new
                {
                    avaliacao_ferida_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprimento = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    largura = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    profundidade = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medida", x => x.avaliacao_ferida_id);
                    table.ForeignKey(
                        name: "FK_medida_avaliacoes_ulcera_avaliacao_ferida_id",
                        column: x => x.avaliacao_ferida_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medida");

            migrationBuilder.CreateTable(
                name: "medidas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    avaliacao_ulcera_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comprimento = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    largura = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    profundidade = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_medidas_avaliacoes_ulcera_avaliacao_ulcera_id",
                        column: x => x.avaliacao_ulcera_id,
                        principalTable: "avaliacoes_ulcera",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_medidas_avaliacao_ulcera_id",
                table: "medidas",
                column: "avaliacao_ulcera_id",
                unique: true);
        }
    }
}
