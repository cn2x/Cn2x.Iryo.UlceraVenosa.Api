using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuplicateTimestampColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ulceras");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Topografias");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Topografias");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Segmentos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Segmentos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RegioesAnatomicas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RegioesAnatomicas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ImagemUlcera");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ImagemUlcera");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fisiologicas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fisiologicas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExsudatoTipos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ExsudatoTipos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Exsudatos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Exsudatos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Etiologicas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Etiologicas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Clinicas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Clinicas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ceaps");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ceaps");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Anatomicas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Anatomicas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ulceras",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ulceras",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Topografias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Topografias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Segmentos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Segmentos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RegioesAnatomicas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RegioesAnatomicas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pacientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pacientes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ImagemUlcera",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ImagemUlcera",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fisiologicas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Fisiologicas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ExsudatoTipos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExsudatoTipos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Exsudatos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Exsudatos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Etiologicas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Etiologicas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Clinicas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Clinicas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ceaps",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ceaps",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Avaliacoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Avaliacoes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Anatomicas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Anatomicas",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
