using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveValueObjectPrefixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SinaisInflamatorios_Rubor",
                table: "Ulceras",
                newName: "Rubor");

            migrationBuilder.RenameColumn(
                name: "SinaisInflamatorios_PerdadeFuncao",
                table: "Ulceras",
                newName: "PerdadeFuncao");

            migrationBuilder.RenameColumn(
                name: "SinaisInflamatorios_Eritema",
                table: "Ulceras",
                newName: "Eritema");

            migrationBuilder.RenameColumn(
                name: "SinaisInflamatorios_Edema",
                table: "Ulceras",
                newName: "Edema");

            migrationBuilder.RenameColumn(
                name: "SinaisInflamatorios_Dor",
                table: "Ulceras",
                newName: "Dor");

            migrationBuilder.RenameColumn(
                name: "SinaisInflamatorios_Calor",
                table: "Ulceras",
                newName: "Calor");

            migrationBuilder.RenameColumn(
                name: "Caracteristicas_TecidoGranulacao",
                table: "Ulceras",
                newName: "TecidoGranulacao");

            migrationBuilder.RenameColumn(
                name: "Caracteristicas_OdorFetido",
                table: "Ulceras",
                newName: "OdorFetido");

            migrationBuilder.RenameColumn(
                name: "Caracteristicas_Necrose",
                table: "Ulceras",
                newName: "Necrose");

            migrationBuilder.RenameColumn(
                name: "Caracteristicas_BordasDefinidas",
                table: "Ulceras",
                newName: "BordasDefinidas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TecidoGranulacao",
                table: "Ulceras",
                newName: "Caracteristicas_TecidoGranulacao");

            migrationBuilder.RenameColumn(
                name: "Rubor",
                table: "Ulceras",
                newName: "SinaisInflamatorios_Rubor");

            migrationBuilder.RenameColumn(
                name: "PerdadeFuncao",
                table: "Ulceras",
                newName: "SinaisInflamatorios_PerdadeFuncao");

            migrationBuilder.RenameColumn(
                name: "OdorFetido",
                table: "Ulceras",
                newName: "Caracteristicas_OdorFetido");

            migrationBuilder.RenameColumn(
                name: "Necrose",
                table: "Ulceras",
                newName: "Caracteristicas_Necrose");

            migrationBuilder.RenameColumn(
                name: "Eritema",
                table: "Ulceras",
                newName: "SinaisInflamatorios_Eritema");

            migrationBuilder.RenameColumn(
                name: "Edema",
                table: "Ulceras",
                newName: "SinaisInflamatorios_Edema");

            migrationBuilder.RenameColumn(
                name: "Dor",
                table: "Ulceras",
                newName: "SinaisInflamatorios_Dor");

            migrationBuilder.RenameColumn(
                name: "Calor",
                table: "Ulceras",
                newName: "SinaisInflamatorios_Calor");

            migrationBuilder.RenameColumn(
                name: "BordasDefinidas",
                table: "Ulceras",
                newName: "Caracteristicas_BordasDefinidas");
        }
    }
}
