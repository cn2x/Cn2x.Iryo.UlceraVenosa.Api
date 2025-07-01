using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUlceraSearchIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Índice para busca por data de exame (para ordenação)
            migrationBuilder.CreateIndex(
                name: "IX_Ulceras_DataExame",
                table: "Ulceras",
                column: "DataExame");

            // Índice composto para busca eficiente de úlceras por paciente
            migrationBuilder.CreateIndex(
                name: "IX_Ulceras_PacienteId_DataExame",
                table: "Ulceras",
                columns: new[] { "PacienteId", "DataExame" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ulceras_DataExame",
                table: "Ulceras");

            migrationBuilder.DropIndex(
                name: "IX_Ulceras_PacienteId_DataExame",
                table: "Ulceras");
        }
    }
}
